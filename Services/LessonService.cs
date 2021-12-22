using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RESTAPIRNSQLServer.Applications.FilterQueries;
using RESTAPIRNSQLServer.DBContext;
using RESTAPIRNSQLServer.DTOs.LessonDTOs;
using RESTAPIRNSQLServer.IServices;
using RESTAPIRNSQLServer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPIRNSQLServer.Services
{    
    public class LessonService : ILessonService
    {
        private readonly AttendenceDBContext _context;
        private readonly IMapper _mapper;

        public LessonService(AttendenceDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<int> AddNewLesson(LessonWriteDTO lessonDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LessonReadDTO>> FilterLessions(FilterLessonItems filterLessionItems)
        {
            var temps = _context.Lessions.AsQueryable().Where(
                item => 
                (!filterLessionItems.From.HasValue || item.StartTime >= filterLessionItems.From) &&
                (!filterLessionItems.To.HasValue || item.EndTime <=filterLessionItems.To)
            );
            var lessons = await temps.ToListAsync();
            var readDTOs = _mapper.Map<IEnumerable<LessonReadDTO>>(lessons);
            return readDTOs;
        }

        public async Task<IEnumerable<LessonReadDTO>> GetAll()
        {
            var lessons = await _context.Lessions.ToListAsync();
            var readDTOs = _mapper.Map<IEnumerable<LessonReadDTO>>(lessons);
            return readDTOs;
        }

        public async Task<LessonReadDTO> GetByID(int id)
        {
            var lesson = await _context.Lessions.Where(item => item.LessonId == id).FirstOrDefaultAsync();
            var readDTO = _mapper.Map<LessonReadDTO>(lesson);
            return readDTO;
        }

        public async Task<LessonReadDTO> GetByName(string name)
        {
            var lesson = await _context.Lessions.Where(item => item.LessonName.Equals(name)).FirstOrDefaultAsync();
            var readDTO = _mapper.Map<LessonReadDTO>(lesson);
            return readDTO;
        }
    }
}
