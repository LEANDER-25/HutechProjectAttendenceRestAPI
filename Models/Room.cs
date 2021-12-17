using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RESTAPIRNSQLServer.Models
{
    public partial class Room
    {
        public Room()
        {
            Schedules = new HashSet<Schedule>();
        }

        [Key]
        [Column("room_id")]
        public int RoomId { get; set; }
        [Required]
        [Column("room_name")]
        [StringLength(25)]
        public string RoomName { get; set; }
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; }

        [InverseProperty(nameof(Schedule.Room))]
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
