using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPIRNSQLServer.Applications.FilterQueries
{
    public class FilterLessonItems
    {
        public TimeSpan? From { get; set; }
        public TimeSpan? To { get; set; }
    }
}
