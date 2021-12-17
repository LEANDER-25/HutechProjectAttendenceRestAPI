using RESTAPIRNSQLServer.Applications.FilterQueries;
using RESTAPIRNSQLServer.DTOs.LessonDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPIRNSQLServer.IServices
{
    public interface ILessonService
    {
        Task<IEnumerable<LessonReadDTO>> GetAll();
        Task<LessonReadDTO> GetByID(int id);
        Task<LessonReadDTO> GetByName(string name);
        Task<int> AddNewLesson(LessonWriteDTO lessonDTO);
        Task<bool> Delete(int id);
        Task<IEnumerable<LessonReadDTO>> FilterLessions(FilterLessonItems filterLessionItems);
    }
}
