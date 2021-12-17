using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RESTAPIRNSQLServer.Models
{
    [Index(nameof(Email), nameof(TeacherCode), Name = "idx_email_teacher", IsUnique = true)]
    public partial class Teacher
    {
        public Teacher()
        {
            Courses = new HashSet<Course>();
        }

        [Key]
        [Column("teacher_id")]
        public int TeacherId { get; set; }
        [Required]
        [Column("teacher_code")]
        [StringLength(25)]
        public string TeacherCode { get; set; }
        [Required]
        [Column("teacher_first_name")]
        [StringLength(255)]
        public string TeacherFirstName { get; set; }
        [Required]
        [Column("teacher_last_name")]
        [StringLength(255)]
        public string TeacherLastName { get; set; }
        [Required]
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [Column("password")]
        [StringLength(100)]
        public string Password { get; set; }

        [InverseProperty("Teacher")]
        public virtual TeacherPhoto TeacherPhoto { get; set; }
        [InverseProperty(nameof(Course.Teacher))]
        public virtual ICollection<Course> Courses { get; set; }
    }
}
