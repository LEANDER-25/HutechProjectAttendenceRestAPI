using System;
using System.Collections.Generic;

namespace RESTAPIRNSQLServer.DTOs.ScheduleDTOs
{
    public class WeeklySchedulesDTO
    {
        public string From { get; set; }
        public string To { get; set; }
        public IEnumerable<PerDay> Schedules { get; set; }

        public class PerDay
        {
            public DateTime StudyDate { get; set; }
            public List<ScheduleReadDTO> Details { get; set; }
        }
    }
}