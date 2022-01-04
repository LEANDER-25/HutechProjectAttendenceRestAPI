namespace RESTAPIRNSQLServer.DTOs.AcademicYearDTOs
{
    public class AcademicYearReadDTO
    {
        public int AyId { get; set; }
        public string AcademicYearName { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
    }
}