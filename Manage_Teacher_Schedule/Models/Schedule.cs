using System;
using System.Collections.Generic;

namespace Manage_Teacher_Schedule.Models
{
    public partial class Schedule
    {
        public int ScheduleId { get; set; }
        public int ClassSubjectId { get; set; }
        public DateTime TeachingDay { get; set; }
        public int SlotId { get; set; }
        public string RoomId { get; set; } = null!;
        public string TeacherId { get; set; } = null!;

        public virtual ClassSubject ClassSubject { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
        public virtual Slot Slot { get; set; } = null!;
        public virtual Teacher Teacher { get; set; } = null!;
    }
}
