using System.Collections.Generic;
using RESTAPIRNSQLServer.Applications.Paginations;

namespace RESTAPIRNSQLServer.DTOs.CheckInDTOs
{
    public class AttendenceReportDTO
    {
        public int TotalStudent { get; set; }
        public int TotalAttendence { get; set; }
        public int ClassId { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public int ScheduleId { get; set; }
        public PaginationResultSet<AttendenceReadDTO> AttendenceList { get; set; }
    }
}