using System;

namespace RESTAPIRNSQLServer.Applications.FilterQueries
{
    public class FilterClassroomItems
    {
        public int? StundentId { get; set; }
        public string StudentCode { get; set; }
        public int? ClassId { get; set; }
        public string ClassName { get; set; }
    }
}