using System.ComponentModel.DataAnnotations;

namespace RESTAPIRNSQLServer.DTOs.CheckInDTOs
{
    public class AttendenceWriteDTO
    {
        [Required]
        public int ClassId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int ScheduleId { get; set; }
        [Required]
        public string StudentCode { get; set; }
        public string Longtitude { get; set; }
        public string Latitude { get; set; }
        public string CurLocation { get; set; }
        [Required]
        public string DeviceId { get; set; }

    }
}