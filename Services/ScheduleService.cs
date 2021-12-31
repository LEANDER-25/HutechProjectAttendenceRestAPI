using System.Threading.Tasks;
using AutoMapper;
using RESTAPIRNSQLServer.Applications.FilterQueries;
using RESTAPIRNSQLServer.Applications.Paginations;
using RESTAPIRNSQLServer.DBContext;
using RESTAPIRNSQLServer.DTOs.ScheduleDTOs;
using RESTAPIRNSQLServer.IServices;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RESTAPIRNSQLServer.Models;
using System.Collections.Generic;
using RESTAPIRNSQLServer.DTOs.LessonDTOs;
using System;
using RESTAPIRNSQLServer.Extensions;

namespace RESTAPIRNSQLServer.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly AttendenceDBContext _context;
        private readonly IMapper _mapper;

        public ScheduleService(AttendenceDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private List<LessonReadDTO> MappingShifts(IEnumerable<StudyShift> entities)
        {
            var shifts = new List<LessonReadDTO>();
            foreach (var item in entities)
            {
                var temp = new LessonReadDTO()
                {
                    LessonId = item.LessonId,
                    LessonName = item.Lesson.LessonName,
                    StartTime = item.Lesson.StartTime,
                    EndTime = item.Lesson.EndTime
                };
                shifts.Add(temp);
            }
            return shifts;
        }
        public async Task<PaginationResultSet<ScheduleReadDTO>> GetAll(PaginationOption pagination)
        {
            var total = await _context.Schedules.CountAsync();
            var scheduleList = await _context.Schedules.AsQueryable()
            .SchedulesOrdered()
            .ScheduleIncludeBottomAssign()
            .SelectScheduleHardCode()
            .Skip((pagination.Page.Value - 1) * pagination.Limit.Value)
            .Take(pagination.Limit.Value)
            .ToListAsync();

            return PaginationResultSet<ScheduleReadDTO>.PackingGoods(pagination.Page.Value, total, pagination.Limit.Value, scheduleList);
        }

        public async Task<ScheduleReadDTO> GetByID(FilterScheduleItems filter)
        {
            var specifySchedule = await _context.Schedules.AsQueryable()
            .Where(
                s => (
                    s.ClassId == filter.ClassId.Value &&
                    s.ScheduleId == filter.ScheduleId.Value &&
                    s.SubjectId == filter.SubjectId.Value &&
                    s.CourseId == filter.CourseId.Value
                )
            )
            .SchedulesOrdered()
            .ScheduleIncludeBottomAssign()
            .ScheduleIncludeRightAssign()
            .ScheduleIncludeLeftAssign()
            .Include(r => r.Room)
            .Select(
                s => new ScheduleReadDTO
                {
                    ScheduleId = s.ScheduleId,
                    CourseId = s.CourseId,
                    ClassId = s.ClassId,
                    ClassName = s.Assign.Class.ClassName,
                    SubjectId = s.SubjectId,
                    SubjectCode = s.Assign.Course.Subject.SubjectCode,
                    SubjectName = s.Assign.Course.Subject.SubjectName,
                    StudyDate = s.StudyDate,
                    RoomId = s.RoomId,
                    Shifts = s.Assign.StudyShifts.Select(
                        item => new LessonReadDTO
                        {
                            LessonId = item.LessonId,
                            LessonName = item.Lesson.LessonName,
                            StartTime = item.Lesson.StartTime,
                            EndTime = item.Lesson.EndTime
                        }
                    ).ToList()
                }
            )
            .FirstOrDefaultAsync();

            // var shifts = await GetStudyShifts(
            //     new FilterScheduleItems
            //     {
            //         ClassId = specifySchedule.ClassId,
            //         CourseId = specifySchedule.CourseId,
            //         SubjectId = specifySchedule.SubjectId
            //     }
            // );

            // specifySchedule.Shifts = MappingShifts(shifts);

            return specifySchedule;
        }

        private async Task<IEnumerable<StudyShift>> GetStudyShifts(FilterScheduleItems filter)
        {
            var shifts = await _context.StudyShifts
            .Where(
                ss => ss.ClassId == filter.ClassId.Value &&
                ss.CourseId == filter.CourseId.Value &&
                ss.SubjectId == filter.SubjectId.Value
            ).Include(ss => ss.Lesson).ToListAsync();
            return shifts;
        }

        public async Task<IEnumerable<ScheduleReadDTO>> GetByStudent(string studentCode)
        {
            #region Get Student ID

            var studentID = await _context.Students
            .Where(s => s.StudentCode.Equals(studentCode))
            .Select(s => s.StudentId)
            .FirstOrDefaultAsync();
            if (studentID == 0)
            {
                return null;
            }

            #endregion

            #region Get Classes of Student
            var joinedClasses = await _context.MainClasses
            .Where(m => m.StudentId == studentID)
            .Include(c => c.Class)
            .Select(c => new Classroom { ClassId = c.ClassId, ClassName = c.Class.ClassName })
            .ToListAsync();

            if (joinedClasses == null)
            {
                return null;
            }
            #endregion

            //Get Schedule in that week
            var dayOfWeek = ((int)DateTime.Now.DayOfWeek);
            ////Monday
            var firstDateOfWeek = DateTime.Now.AddDays(0 - (dayOfWeek - 1));
            ////Sunday
            var lastDateOfWeek = DateTime.Now.AddDays(6 - dayOfWeek + 1);

            //Create the query and result
            var query = _context.Schedules.AsQueryable();
            var schedules = new List<ScheduleReadDTO>();

            //Find in Schedule table which map with classID and in that week
            foreach (var item in joinedClasses)
            {
                var temp = await query.AsQueryable()
                .Where(
                    s => (
                        s.ClassId == item.ClassId &&
                        s.StudyDate >= firstDateOfWeek &&
                        s.StudyDate <= lastDateOfWeek
                    )
                )
                .ScheduleIncludeBottomAssign()
                .Include(r => r.Room)
                .Select(
                    s => new ScheduleReadDTO
                    {
                        ScheduleId = s.ScheduleId,
                        CourseId = s.CourseId,
                        ClassId = s.ClassId,
                        ClassName = item.ClassName,
                        SubjectId = s.SubjectId,
                        SubjectCode = s.Assign.Course.Subject.SubjectCode,
                        SubjectName = s.Assign.Course.Subject.SubjectName,
                        StudyDate = s.StudyDate,
                        RoomId = s.RoomId,
                        RoomName = s.Room.RoomName
                    }
                ).ToListAsync();

                if (temp == null)
                {
                    continue;
                }

                //Found and add into result
                schedules.AddRange(temp);
            }

            //sort the studyDate of the result
            schedules = schedules.OrderBy(s => s.StudyDate).ToList();

            //Find the shifts of schedule
            foreach (var schedule in schedules)
            {
                var shifts = await GetStudyShifts(
                    new FilterScheduleItems
                    {
                        ClassId = schedule.ClassId,
                        CourseId = schedule.CourseId,
                        SubjectId = schedule.SubjectId
                    }
                );
                schedule.Shifts = MappingShifts(shifts);
            }

            return schedules;
        }
    }
}