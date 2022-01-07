using System.Collections.Generic;

namespace RESTAPIRNSQLServer.DTOs.ScheduleDTOs
{
    public class WeeklySchedulesDTO
    {
        public string From { get; set; }
        public string To { get; set; }
        public IEnumerable<ScheduleReadDTO> Schedules { get; set; }
    }
}