using System.ComponentModel.DataAnnotations;

namespace RESTAPIRNSQLServer.DTOs.SystemDTOs
{
    public class StudentRegisterDTO
    {
        [Required]
        public string BusinessCode { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        public string ClassName { get; set; }

        [Required]
        public string AcademicYear { get; set; }
    }
}