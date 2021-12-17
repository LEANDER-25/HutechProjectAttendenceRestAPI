using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RESTAPIRNSQLServer.Models
{
    [Table("Check_Ins_Photos")]
    public partial class CheckInsPhoto
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
        [Column("photo", TypeName = "image")]
        public byte[] Photo { get; set; }

        [ForeignKey("StudentId,SubjectId,CourseId,ClassId,ScheduleId")]
        [InverseProperty("CheckInsPhoto")]
        public virtual CheckIn CheckIn { get; set; }
    }
}
