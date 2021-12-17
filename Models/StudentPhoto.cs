using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RESTAPIRNSQLServer.Models
{
    [Table("Student_Photos")]
    public partial class StudentPhoto
    {
        [Key]
        [Column("student_id")]
        public int StudentId { get; set; }
        [Column("photo", TypeName = "image")]
        public byte[] Photo { get; set; }

        [ForeignKey(nameof(StudentId))]
        [InverseProperty("StudentPhoto")]
        public virtual Student Student { get; set; }
    }
}
