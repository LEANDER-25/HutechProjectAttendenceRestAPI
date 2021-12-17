using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RESTAPIRNSQLServer.Models
{
    [Table("Study_Shifts")]
    public partial class StudyShift
    {
        [Key]
        [Column("lesson_id")]
        public int LessonId { get; set; }
        [Key]
        [Column("class_id")]
        public int ClassId { get; set; }
        [Key]
        [Column("course_id")]
        public int CourseId { get; set; }
        [Key]
        [Column("subject_id")]
        public int SubjectId { get; set; }

        [ForeignKey("ClassId,CourseId,SubjectId")]
        [InverseProperty("StudyShifts")]
        public virtual Assign Assign { get; set; }
        [ForeignKey(nameof(LessonId))]
        [InverseProperty(nameof(Lession.StudyShifts))]
        public virtual Lession Lesson { get; set; }
    }
}
