using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPIRNSQLServer.DTOs.LessonDTOs
{
    public class LessonReadDTO
    {
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
