using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RESTAPIRNSQLServer.Models
{
    public partial class Classroom
    {
        public Classroom()
        {
            Assigns = new HashSet<Assign>();
            MainClasses = new HashSet<MainClass>();
        }

        [Key]
        [Column("class_id")]
        public int ClassId { get; set; }
        [Required]
        [Column("class_name")]
        [StringLength(25)]
        public string ClassName { get; set; }
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; }

        [InverseProperty(nameof(Assign.Class))]
        public virtual ICollection<Assign> Assigns { get; set; }
        [InverseProperty(nameof(MainClass.Class))]
        public virtual ICollection<MainClass> MainClasses { get; set; }
    }
}
