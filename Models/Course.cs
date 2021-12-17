using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RESTAPIRNSQLServer.Models
{
    public partial class Course
    {
        public Course()
        {
            Assigns = new HashSet<Assign>();
        }

        [Key]
        [Column("course_id")]
        public int CourseId { get; set; }
        [Key]
        [Column("subject_id")]
        public int SubjectId { get; set; }
        [Column("point")]
        public double? Point { get; set; }
        [Column("semester")]
        public int? Semester { get; set; }
        [Column("year")]
        public int? Year { get; set; }
        [Column("teacher_id")]
        public int? TeacherId { get; set; }

        [ForeignKey(nameof(SubjectId))]
        [InverseProperty("Courses")]
        public virtual Subject Subject { get; set; }
        [ForeignKey(nameof(TeacherId))]
        [InverseProperty("Courses")]
        public virtual Teacher Teacher { get; set; }
        [InverseProperty(nameof(Assign.Course))]
        public virtual ICollection<Assign> Assigns { get; set; }
    }
}
