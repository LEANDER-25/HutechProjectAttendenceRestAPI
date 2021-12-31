using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using RESTAPIRNSQLServer.Applications.FilterQueries;
using RESTAPIRNSQLServer.DTOs.ClassDTOs;

namespace RESTAPIRNSQLServer.IServices
{
    public interface IClassService
    {
        Task<IEnumerable<ClassReadDTO>> GetAllClass();
        Task<IEnumerable<ClassReadDTO>> GetClassByStudent(string studentCode);
        Task<ClassReadDTO> GetSpecifyClass(FilterClassroomItems classFilter);
    }
}