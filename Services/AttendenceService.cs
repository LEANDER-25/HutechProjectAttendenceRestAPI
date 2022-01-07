using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RESTAPIRNSQLServer.Applications.FilterQueries;
using RESTAPIRNSQLServer.Applications.Logic;
using RESTAPIRNSQLServer.Applications.Paginations;
using RESTAPIRNSQLServer.DBContext;
using RESTAPIRNSQLServer.DTOs.CheckInDTOs;
using RESTAPIRNSQLServer.Extensions;
using RESTAPIRNSQLServer.IServices;
using RESTAPIRNSQLServer.Models;

namespace RESTAPIRNSQLServer.Services
{
    public class AttendenceService : IAttendenceService
    {
        private readonly AttendenceDBContext _context;
        private readonly IMapper _mapper;
        private readonly IScheduleService _scheduleService;

        public AttendenceService(AttendenceDBContext context, IMapper mapper, IScheduleService scheduleService)
        {
            _context = context;
            _mapper = mapper;
            _scheduleService = scheduleService;
        }
        private IQueryable<CheckIn> GetAllRecordOfSchedule(FilterScheduleItems filter)
        {
            var list = _context.CheckIns.AsQueryable()
            .OrderBy(s => s.ClassId)
            .OrderByDescending(s => s.CreatedAt)
            .Where(
                a => (
                    a.ClassId == filter.ClassId &&
                    a.CourseId == filter.CourseId &&
                    a.SubjectId == filter.SubjectId &&
                    a.ScheduleId == filter.ScheduleId
                )
            );
            return list;
        }
        public async Task<PaginationResultSet<AttendenceReadDTO>> GetAttendenceListBySchedule(FilterScheduleItems filter, PaginationOption option)
        {
            // var attendences = _context.CheckIns.AsQueryable();
            int total = await GetAllRecordOfSchedule(filter).CountAsync();

            var list = await GetAllRecordOfSchedule(filter)
            .Include(a => a.Student)
            .Skip((option.Page.Value - 1) * option.Limit.Value)
            .Take(option.Limit.Value)
            .Select(
                a => new AttendenceReadDTO()
                {
                    ClassId = a.ClassId,
                    ScheduleId = a.ScheduleId,
                    SubjectId = a.SubjectId,
                    CourseId = a.CourseId,
                    CreatedAt = a.CreatedAt,
                    CurLocation = a.CurLocation,
                    StudentId = a.StudentId,
                    StudentCode = a.Student.StudentCode,
                    FirstName = a.Student.FirstName,
                    LastName = a.Student.LastName,
                    DeviceId = a.DeviceId
                }
            ).ToListAsync();

            if (list == null)
            {
                throw new System.Exception("No Attendence list of this schedule");
            }

            return PaginationResultSet<AttendenceReadDTO>.PackingGoods(option.Page.Value, total, option.Limit.Value, list);
        }
        public async Task<AttendenceReportDTO> GetAttendenceReportBySchedule(FilterScheduleItems filter, PaginationOption option)
        {
            var totalStudent = await _context.MainClasses.Where(
                c => c.ClassId == filter.ClassId
            ).CountAsync();
            var attendencePagination = await GetAttendenceListBySchedule(filter, option);
            var report = new AttendenceReportDTO{
                TotalStudent = totalStudent,
                TotalAttendence = attendencePagination.Pagination.TotalRows,
                AttendenceList = attendencePagination
            };
            return report;
        }

        public async Task<AttendenceReadDTO> GetSingleRecordAttendence(FilterAttendenceItems filter)
        {
            var attendences = _context.CheckIns.AsQueryable();
            var single = await attendences.Where(
                a => (
                    a.StudentId == filter.StundentId.Value &&
                    a.CreatedAt == filter.CreatedAt.Value
                )
            )
            .Include(s => s.Student)
            .Select(
                a => new AttendenceReadDTO()
                {
                    ClassId = a.ClassId,
                    ScheduleId = a.ScheduleId,
                    SubjectId = a.SubjectId,
                    CourseId = a.CourseId,
                    CreatedAt = a.CreatedAt,
                    CurLocation = a.CurLocation,
                    StudentId = a.StudentId,
                    StudentCode = a.Student.StudentCode,
                    FirstName = a.Student.FirstName,
                    LastName = a.Student.LastName,
                    DeviceId = a.DeviceId
                }
            ).FirstOrDefaultAsync();

            return single;
        }

        public async Task<bool> InsertNewRecord(AttendenceWriteDTO newAttendence)
        {
            //Find student
            var studentId = await _context.Students.Where(
                s => s.StudentCode.Equals(newAttendence.StudentCode)
            ).Select(s => s.StudentId).FirstOrDefaultAsync();
            if (studentId == 0)
            {
                throw new Exception("Not found student");
            }

            //Find class which student registered
            var isClassExist = await _context.MainClasses.Where(s => s.StudentId == studentId).AnyAsync(c => c.ClassId == newAttendence.ClassId);

            if (isClassExist is false)
            {
                throw new Exception("Not found class or student did not register to the class");
            }

            //Validate location of request vs location of room
            // var isInRange = GeocodingMath.IsYInRangeOfX("x", "y");

            //Validate create attendence time
            //Find schedule and make sure that schedule of request is avaible in the create time of request
            //If the create attendence time of new record is SMALLER than the start time of schedule 
            //  (create: 14:50 - schedule: 15:00)=> fail
            //If the create attendence time of new record is GREATER then the start time of schedule
            //  Greater than but in range: 0-15 min => success
            //  Greater than but out of range: 0-15 min => fail
            var fitler = new FilterScheduleItems()
            {
                ClassId = newAttendence.ClassId,
                ScheduleId = newAttendence.ScheduleId,
                SubjectId = newAttendence.SubjectId,
                CourseId = newAttendence.CourseId
            };
            var schedule = await _scheduleService.GetByID(
                fitler
            );

            var room = await _context.Rooms.Where(r => r.RoomId == schedule.RoomId).FirstOrDefaultAsync();
            ValidationInput.ValidateLocation(newAttendence, room);

            var createAttendenceTime = DateTime.Now;
            var scheduleDate = schedule.StudyDate.Value.Date;

            if (ValidAttendenceTime.IsScheduleAvailable(createAttendenceTime.Date, scheduleDate) is false)
            {
                throw new Exception("This course is not available now or the study date is not today");
            }

            var scheduleStartTime = schedule.Shifts[0].StartTime;
            ValidAttendenceTime.IsCheckInAvaible(createAttendenceTime.TimeOfDay, scheduleStartTime);

            //Make sure that that in the same schedule there no device duplicate
            var checkinlist = await GetAllRecordOfSchedule(fitler).Include(s => s.Student).Select(
                c => new AttendenceReadDTO()
                {
                    StudentId = c.StudentId,
                    StudentCode = c.Student.StudentCode,
                    CreatedAt = c.CreatedAt,
                    DeviceId = c.DeviceId
                }
            ).ToListAsync();

            foreach (var item in checkinlist)
            {
                if (newAttendence.DeviceId.Equals(item.DeviceId))
                {
                    throw new Exception($"Duplicate device, you are using same device with student {item.StudentCode}");
                }
            }

            var checkin = _mapper.Map<CheckIn>(newAttendence);
            checkin.CreatedAt = createAttendenceTime;
            checkin.SubjectId = studentId;

            await _context.CheckIns.AddAsync(checkin);
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<IEnumerable<AttendenceDetailDTO>> GetAttendencesOfSpecifyStudent(string code)
        {
            var studentId = await _context.Students.Where(s => s.StudentCode.Equals(code)).Select(s => s.StudentId).FirstOrDefaultAsync();
            var attendenceList = await _context.CheckIns
            .OrderBy(c => c.ClassId)
            .OrderByDescending(c => c.CreatedAt)
            .Where(
                record => record.StudentId == studentId
            ).ToListAsync();
            var classrooms = await _context.Classrooms
            .OrderBy(c => c.ClassId)
            .Select( 
                c =>  new Classroom 
                { 
                    ClassId = c.ClassId, 
                    ClassName = c.ClassName 
                }
            ).ToListAsync();
            var tempClasses = classrooms.Select(c => c.ClassId).ToList();
            var subjects = await _context.Subjects
            .OrderBy(s => s.SubjectId)
            .ToListAsync();
            var tempSubjects = subjects.Select(s => s.SubjectId).ToList();
            var attendenceReport = new List<AttendenceDetailDTO>();
            foreach (var item in attendenceList)
            {
                var classPos = Tool.BinarySearch<int>(tempClasses, item.ClassId);
                var subjectPos = Tool.BinarySearch<int>(tempSubjects, item.SubjectId);
                var temp = new AttendenceDetailDTO() 
                {
                    ClassId = item.ClassId,
                    ClassName = classrooms[classPos].ClassName,
                    SubjectId = item.SubjectId,
                    SubjectName = subjects[subjectPos].SubjectName,
                    CourseId = item.CourseId,
                    ScheduleId = item.ScheduleId,
                    StudentId = item.StudentId,
                    CreatedAt = item.CreatedAt,
                };
                attendenceReport.Add(temp);
            }
            return attendenceReport;
        }        
    }
}
