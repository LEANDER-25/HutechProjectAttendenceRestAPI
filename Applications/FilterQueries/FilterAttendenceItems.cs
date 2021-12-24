using System;

namespace RESTAPIRNSQLServer.Applications.FilterQueries
{
    public class FilterAttendenceItems : FilterScheduleItems
    {
        public int? StundentId { get; set; }
        public string StudentCode { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}