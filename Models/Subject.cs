using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RESTAPIRNSQLServer.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Courses = new HashSet<Course>();
        }

        [Key]
        [Column("subject_id")]
        public int SubjectId { get; set; }
        [Required]
        [Column("subject_code")]
        [StringLength(25)]
        public string SubjectCode { get; set; }
        [Required]
        [Column("subject_name")]
        [StringLength(255)]
        public string SubjectName { get; set; }

        [InverseProperty(nameof(Course.Subject))]
        public virtual ICollection<Course> Courses { get; set; }
    }
}
