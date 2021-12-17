using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RESTAPIRNSQLServer.Models
{
    [Table("Teacher_Photos")]
    public partial class TeacherPhoto
    {
        [Key]
        [Column("teacher_id")]
        public int TeacherId { get; set; }
        [Column("photos", TypeName = "image")]
        public byte[] Photos { get; set; }

        [ForeignKey(nameof(TeacherId))]
        [InverseProperty("TeacherPhoto")]
        public virtual Teacher Teacher { get; set; }
    }
}
