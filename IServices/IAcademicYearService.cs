using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using RESTAPIRNSQLServer.Applications.FilterQueries;
using RESTAPIRNSQLServer.DTOs.AcademicYearDTOs;
using RESTAPIRNSQLServer.DTOs.ClassDTOs;

namespace RESTAPIRNSQLServer.IServices
{
    public interface IAcademicYearService
    {
        Task<IEnumerable<AcademicYearReadDTO>> GetAllYears();
        Task<AcademicYearReadDTO> GetSpecifyYears(string yearName);
    }
}