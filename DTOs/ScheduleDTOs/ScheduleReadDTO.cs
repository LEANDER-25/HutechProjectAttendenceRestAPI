using System;
using System.Collections.Generic;
using RESTAPIRNSQLServer.DTOs.LessonDTOs;

namespace RESTAPIRNSQLServer.DTOs.ScheduleDTOs
{
    public class ScheduleReadDTO
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public int ScheduleId { get; set; }
        public DateTime? StudyDate { get; set; }
        public int? RoomId { get; set; }
        public string RoomName { get; set; }
        public List<LessonReadDTO> Shifts { get; set;}
    }
}