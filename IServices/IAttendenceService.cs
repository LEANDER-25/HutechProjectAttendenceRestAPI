using System.Collections.Generic;
using System.Threading.Tasks;
using RESTAPIRNSQLServer.Applications.FilterQueries;
using RESTAPIRNSQLServer.Applications.Paginations;
using RESTAPIRNSQLServer.DTOs.CheckInDTOs;

namespace RESTAPIRNSQLServer.IServices
{
    public interface IAttendenceService
    {
        Task<PaginationResultSet<AttendenceReadDTO>> GetAttendenceListBySchedule(FilterScheduleItems filter, PaginationOption option);
        Task<AttendenceReadDTO> GetSingleRecordAttendence(FilterAttendenceItems filter);
        Task<bool> InsertNewRecord(AttendenceWriteDTO newAttendence);
        Task<IEnumerable<AttendenceDetailDTO>> GetAttendencesOfSpecifyStudent(string code);
    }
}