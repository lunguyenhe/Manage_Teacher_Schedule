using System;
using System.Collections.Generic;

namespace Manage_Teacher_Schedule.Models
{
    public partial class ClassSubject
    {
        public ClassSubject()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int ClassSubjectId { get; set; }
        public string? ClassId { get; set; }
        public string? SubjectId { get; set; }

        public virtual Class? Class { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
