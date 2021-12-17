using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RESTAPIRNSQLServer.Models
{
    [Table("Check_Ins")]
    public partial class CheckIn
    {
        [Key]
        [Column("student_id")]
        public int StudentId { get; set; }
        [Key]
        [Column("subject_id")]
        public int SubjectId { get; set; }
        [Key]
        [Column("course_id")]
        public int CourseId { get; set; }
        [Key]
        [Column("class_id")]
        public int ClassId { get; set; }
        [Key]
        [Column("schedule_id")]
        public int ScheduleId { get; set; }
        [Required]
        [Column("cur_location")]
        [StringLength(500)]
        public string CurLocation { get; set; }

        [ForeignKey("SubjectId,CourseId,ClassId,ScheduleId")]
        [InverseProperty("CheckIns")]
        public virtual Schedule Schedule { get; set; }
        [ForeignKey(nameof(StudentId))]
        [InverseProperty("CheckIns")]
        public virtual Student Student { get; set; }
        [InverseProperty("CheckIn")]
        public virtual CheckInsPhoto CheckInsPhoto { get; set; }
    }
}
