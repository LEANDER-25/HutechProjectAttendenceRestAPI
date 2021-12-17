using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RESTAPIRNSQLServer.Models
{
    [Index(nameof(Email), Name = "idx_email_student", IsUnique = true)]
    [Index(nameof(StudentCode), Name = "idx_student_id", IsUnique = true)]
    public partial class Student
    {
        public Student()
        {
            CheckIns = new HashSet<CheckIn>();
            MainClasses = new HashSet<MainClass>();
        }

        [Key]
        [Column("student_id")]
        public int StudentId { get; set; }
        [Required]
        [Column("student_code")]
        [StringLength(25)]
        public string StudentCode { get; set; }
        [Required]
        [Column("first_name")]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [Column("last_name")]
        [StringLength(255)]
        public string LastName { get; set; }
        [Required]
        [Column("password")]
        [StringLength(100)]
        public string Password { get; set; }
        [Required]
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; }
        [Column("academic_year")]
        public int AcademicYear { get; set; }

        [ForeignKey(nameof(AcademicYear))]
        [InverseProperty("Students")]
        public virtual AcademicYear AcademicYearNavigation { get; set; }
        [InverseProperty("Student")]
        public virtual StudentPhoto StudentPhoto { get; set; }
        [InverseProperty(nameof(CheckIn.Student))]
        public virtual ICollection<CheckIn> CheckIns { get; set; }
        [InverseProperty(nameof(MainClass.Student))]
        public virtual ICollection<MainClass> MainClasses { get; set; }
    }
}
