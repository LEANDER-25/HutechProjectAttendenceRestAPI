using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RESTAPIRNSQLServer.Models
{
    [Table("Academic_Years")]
    public partial class AcademicYear
    {
        public AcademicYear()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        [Column("ay_id")]
        public int AyId { get; set; }
        [Required]
        [Column("academic_year_name")]
        [StringLength(25)]
        public string AcademicYearName { get; set; }
        [Column("start_year")]
        public int StartYear { get; set; }
        [Column("end_year")]
        public int EndYear { get; set; }

        [InverseProperty(nameof(Student.AcademicYearNavigation))]
        public virtual ICollection<Student> Students { get; set; }
    }
}
