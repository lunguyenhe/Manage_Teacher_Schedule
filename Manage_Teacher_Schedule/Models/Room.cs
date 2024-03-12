using System;
using System.Collections.Generic;

namespace Manage_Teacher_Schedule.Models
{
    public partial class Room
    {
        public Room()
        {
            Schedules = new HashSet<Schedule>();
        }

        public string RoomId { get; set; } = null!;
        public string Capacity { get; set; } = null!;

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
