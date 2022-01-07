using System.Collections.Generic;
using System.Threading.Tasks;
using RESTAPIRNSQLServer.Applications.FilterQueries;
using RESTAPIRNSQLServer.Applications.Paginations;
using RESTAPIRNSQLServer.DTOs.ScheduleDTOs;

namespace RESTAPIRNSQLServer.IServices
{
    public interface IScheduleService
    {
        //GET all schedule
        Task<PaginationResultSet<ScheduleReadDTO>> GetAll(PaginationOption pagination);
        //GET each schedule
        Task<ScheduleReadDTO> GetByID(FilterScheduleItems filter);
        Task<WeeklySchedulesDTO> GetByStudent(string studentCode);
        Task<WeeklySchedulesDTO> GetByTeacher(string teacherCode);
    }
}