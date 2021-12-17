using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RESTAPIRNSQLServer.Models
{
    [Table("Main_Classes")]
    public partial class MainClass
    {
        [Key]
        [Column("class_id")]
        public int ClassId { get; set; }
        [Key]
        [Column("student_id")]
        public int StudentId { get; set; }

        [ForeignKey(nameof(ClassId))]
        [InverseProperty(nameof(Classroom.MainClasses))]
        public virtual Classroom Class { get; set; }
        [ForeignKey(nameof(StudentId))]
        [InverseProperty("MainClasses")]
        public virtual Student Student { get; set; }
    }
}
