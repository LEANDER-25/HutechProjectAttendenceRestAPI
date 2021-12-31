using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RESTAPIRNSQLServer.DBContext;
using RESTAPIRNSQLServer.DTOs.ClassDTOs;
using RESTAPIRNSQLServer.IServices;
using RESTAPIRNSQLServer.Models;
using RESTAPIRNSQLServer.Applications.FilterQueries;

namespace RESTAPIRNSQLServer.Services
{
    public class ClassroomService : IClassService
    {
        private readonly AttendenceDBContext _context;
        private readonly IMapper _mapper;

        public ClassroomService(AttendenceDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ClassReadDTO>> GetAllClass()
        {
            var classrooms = await _context.Classrooms.ToListAsync();
            var classDto = _mapper.Map<IEnumerable<ClassReadDTO>>(classrooms);
            return classDto;
        }

        public async Task<IEnumerable<ClassReadDTO>> GetClassByStudent(string studentCode)
        {
            var studentId = await _context.Students.Where(s => s.StudentCode.Equals(studentCode)).Select(s => s.StudentId).FirstOrDefaultAsync();
            var classrooms = await _context.MainClasses
            .Where(m => m.StudentId == studentId)
            .Include(c => c.Class)
            .Select(
                c => new ClassReadDTO{
                    ClassId = c.ClassId,
                    ClassName = c.Class.ClassName
                }
            ).ToListAsync();
            return classrooms;
        }

        public async Task<ClassReadDTO> GetSpecifyClass(FilterClassroomItems classFilter)
        {
            var classroom = await _context.Classrooms.Where(c => 
                (!classFilter.ClassId.HasValue || c.ClassId == c.ClassId) && 
                (!classFilter.ClassName.Equals(null) || c.ClassName.Equals(classFilter.ClassName))
            )
            .Select(
                c => new ClassReadDTO{
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    Description = c.Description
                }
            )
            .FirstOrDefaultAsync();
            return classroom;
        }
    }
}