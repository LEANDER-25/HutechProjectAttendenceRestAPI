using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RESTAPIRNSQLServer.Models
{
    public partial class Lession
    {
        public Lession()
        {
            StudyShifts = new HashSet<StudyShift>();
        }

        [Key]
        [Column("lesson_id")]
        public int LessonId { get; set; }
        [Required]
        [Column("lesson_name")]
        [StringLength(255)]
        public string LessonName { get; set; }
        [Column("start_time")]
        public TimeSpan StartTime { get; set; }
        [Column("end_time")]
        public TimeSpan EndTime { get; set; }

        [InverseProperty(nameof(StudyShift.Lesson))]
        public virtual ICollection<StudyShift> StudyShifts { get; set; }
    }
}
