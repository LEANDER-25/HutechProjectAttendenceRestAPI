using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPIRNSQLServer.DTOs.CheckInDTOs
{
    public class AttendenceDetailDTO
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int ScheduleId { get; set; }
        public int StudentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PhotoPath { get; set; }
    }
}
