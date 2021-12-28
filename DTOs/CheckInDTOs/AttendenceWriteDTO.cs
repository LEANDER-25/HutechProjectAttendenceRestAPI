namespace RESTAPIRNSQLServer.DTOs.CheckInDTOs
{
    public class AttendenceWriteDTO
    {
        public int ClassId { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public int ScheduleId { get; set; }
        public string StudentCode { get; set; }
        public string Longtitude { get; set; }
        public string Latitude { get; set; }
        public string CurLocation { get; set; }
        public string DeviceId { get; set; }

    }
}