using System;

namespace RESTAPIRNSQLServer.DTOs.CheckInDTOs
{
    public class AttendenceReadDTO
    {
        public int ClassId { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public int ScheduleId { get; set; }
        public int StudentId { get; set; }
        public string StudentCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CurLocation { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PhotoPath { get; set; }
        public string DeviceId { get; set; }
    }
}