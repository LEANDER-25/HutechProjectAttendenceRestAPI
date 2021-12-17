using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RESTAPIRNSQLServer.Models
{
    public partial class Assign
    {
        public Assign()
        {
            Schedules = new HashSet<Schedule>();
            StudyShifts = new HashSet<StudyShift>();
        }

        [Key]
        [Column("class_id")]
        public int ClassId { get; set; }
        [Key]
        [Column("course_id")]
        public int CourseId { get; set; }
        [Key]
        [Column("subject_id")]
        public int SubjectId { get; set; }

        [ForeignKey(nameof(ClassId))]
        [InverseProperty(nameof(Classroom.Assigns))]
        public virtual Classroom Class { get; set; }
        [ForeignKey("CourseId,SubjectId")]
        [InverseProperty("Assigns")]
        public virtual Course Course { get; set; }
        [InverseProperty(nameof(Schedule.Assign))]
        public virtual ICollection<Schedule> Schedules { get; set; }
        [InverseProperty(nameof(StudyShift.Assign))]
        public virtual ICollection<StudyShift> StudyShifts { get; set; }
    }
}
