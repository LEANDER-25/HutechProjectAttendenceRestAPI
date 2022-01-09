using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RESTAPIRNSQLServer.Applications.FilterQueries;
using RESTAPIRNSQLServer.Applications.Logic;
using RESTAPIRNSQLServer.Applications.System;
using RESTAPIRNSQLServer.DBContext;
using RESTAPIRNSQLServer.DTOs.ImageDTOs;
using RESTAPIRNSQLServer.IServices;
using RESTAPIRNSQLServer.Models;

namespace RESTAPIRNSQLServer.Services
{
    public class ImageService : IImageService
    {
        private readonly AttendenceDBContext _context;
        private readonly IMapper _mapper;
        private readonly IAttendenceService _attendenceService;

        public ImageService(AttendenceDBContext context, IMapper mapper, IAttendenceService attendenceService)
        {
            _context = context;
            _mapper = mapper;
            _attendenceService = attendenceService;
        }
        private async Task<byte[]> ConvertImageToBytes(IFormFile imageUpload)
        {
            if (imageUpload.Length > 0)
            {
                if (imageUpload.Length > 2000000)
                {
                    throw new System.Exception("Image is greater than 2 MB");
                }
                using (var ms = new MemoryStream())
                {
                    imageUpload.CopyTo(ms);
                    var bytes = ms.ToArray();
                    await ms.FlushAsync();
                    return bytes;
                }
            }
            else
            {
                throw new System.Exception("Missing Image");
            }
        }
        private byte[] ConvertBytesToImage(string base64string)
        {
            byte[] bytes = null;
            if (string.IsNullOrEmpty(base64string) is false)
            {
                bytes = Convert.FromBase64String(base64string);
            }
            return bytes;
        }

        //Attendence
        public async Task<string> SaveAttendenceImage(ImageUploadDTO uploadDTO)
        {
            if (uploadDTO.EntityType != EntityEnum.Attendence)
            {
                throw new System.Exception("Wrong Type of Entity");
            }
            uploadDTO.BusinessCode = uploadDTO.BusinessCode.Trim();
            var splits = uploadDTO.BusinessCode.Split("-");
            var attendenceReadDTO = await _attendenceService.GetSingleRecordAttendence(
                new FilterAttendenceItems()
                {
                    StudentCode = splits[0],
                    ScheduleId = Int32.Parse(splits[1])
                }
            );
            if (attendenceReadDTO == null)
            {
                throw new Exception("Can not find the given record");
            }
            var bytes = await ConvertImageToBytes(uploadDTO.Image);

            var attendencePhoto = await _context.CheckInsPhotos.Where(
                p => (
                    p.ClassId == attendenceReadDTO.ClassId &&
                    p.CourseId == attendenceReadDTO.CourseId &&
                    p.StudentId == attendenceReadDTO.StudentId &&
                    p.SubjectId == attendenceReadDTO.SubjectId &&
                    p.ScheduleId == attendenceReadDTO.ScheduleId
                )
            )
            .FirstOrDefaultAsync();

            if (attendencePhoto != null && attendencePhoto.Photo.Equals(bytes))
            {
                throw new Exception("The image of this record is existed");
            }
            await _context.CheckInsPhotos.AddAsync(
                new CheckInsPhoto()
                {
                    ClassId = attendenceReadDTO.ClassId,
                    CourseId = attendenceReadDTO.CourseId,
                    StudentId = attendenceReadDTO.StudentId,
                    SubjectId = attendenceReadDTO.SubjectId,
                    ScheduleId = attendenceReadDTO.ScheduleId,
                    Photo = bytes
                }
            );
            await _context.SaveChangesAsync();

            //Subject-Course-Class-Schedule-Student : Ex : 1.2.3.4.5
            var imageName = $"{attendenceReadDTO.SubjectId}.{attendenceReadDTO.CourseId}.{attendenceReadDTO.ClassId}.{attendenceReadDTO.ScheduleId}.{attendenceReadDTO.StudentId}";
            return imageName;
        }
        public async Task<byte[]> GetAttendenceImage(string imageName)
        {
            // if (ValidationInput.IsImageNameValid(imageName) is false)
            // {
            //     throw new Exception("Invalid image's name");
            // }

            var spiltImageName = imageName.Split(".");

            var subjectId = Int32.Parse(spiltImageName[0]);
            var courseId = Int32.Parse(spiltImageName[1]);
            var classId = Int32.Parse(spiltImageName[2]);
            var scheduleId = Int32.Parse(spiltImageName[3]);
            var studentId = Int32.Parse(spiltImageName[4]);

            var attendencePhoto = await _context.CheckInsPhotos.Where(
                p => (
                    p.ClassId == classId &&
                    p.CourseId == courseId &&
                    p.StudentId == studentId &&
                    p.SubjectId == subjectId &&
                    p.ScheduleId == scheduleId
                )
            )
            .FirstOrDefaultAsync();

            if (attendencePhoto == null)
            {
                throw new Exception("Can not find the record");
            }
            return ConvertBytesToImage(Convert.ToBase64String(attendencePhoto.Photo));
        }
        public async Task<string> SaveStudentImage(ImageUploadDTO uploadDTO)
        {
            if (uploadDTO.EntityType != EntityEnum.Stundent)
            {
                throw new System.Exception("Wrong Type of Entity");
            }
            uploadDTO.BusinessCode = uploadDTO.BusinessCode.Trim();

            var student = await _context.Students.Where(s => s.StudentCode.Equals(uploadDTO.BusinessCode)).FirstOrDefaultAsync();

            if (student == null)
            {
                throw new Exception("Can not find the student");
            }
            var bytes = await ConvertImageToBytes(uploadDTO.Image);

            var studentPhoto = await _context.StudentPhotos.Where(sp => sp.StudentId == student.StudentId).FirstOrDefaultAsync();

            if (studentPhoto != null)
            {

                studentPhoto.Photo = bytes;
                await _context.SaveChangesAsync();
            }
            else
            {
                await _context.StudentPhotos.AddAsync(
                    new StudentPhoto()
                    {
                        StudentId = student.StudentId,
                        Photo = bytes
                    }
                );
                await _context.SaveChangesAsync();
            }

            return student.StudentId.ToString();
        }

        public async Task<byte[]> GetStudentImage(string studentID)
        {
            if (ValidationInput.AreDigitsOnly(studentID) is false)
            {
                throw new Exception("Invalid image's name");
            }

            var studentPhoto = await _context.StudentPhotos.Where(
                p => p.StudentId == Int32.Parse(studentID)
            )
            .FirstOrDefaultAsync();

            if (studentPhoto == null)
            {
                throw new Exception("Can not find the image");
            }
            return ConvertBytesToImage(Convert.ToBase64String(studentPhoto.Photo));
        }
    }
}