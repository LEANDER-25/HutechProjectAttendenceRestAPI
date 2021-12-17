using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RESTAPIRNSQLServer.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            CheckIns = new HashSet<CheckIn>();
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
        [Key]
        [Column("schedule_id")]
        public int ScheduleId { get; set; }
        [Column("study_date", TypeName = "date")]
        public DateTime? StudyDate { get; set; }
        [Column("room_id")]
        public int? RoomId { get; set; }

        [ForeignKey("ClassId,CourseId,SubjectId")]
        [InverseProperty("Schedules")]
        public virtual Assign Assign { get; set; }
        [ForeignKey(nameof(RoomId))]
        [InverseProperty("Schedules")]
        public virtual Room Room { get; set; }
        [InverseProperty(nameof(CheckIn.Schedule))]
        public virtual ICollection<CheckIn> CheckIns { get; set; }
    }
}
