using System;
using System.Collections.Generic;

namespace Manage_Teacher_Schedule.Models
{
    public partial class Subject
    {
        public Subject()
        {
            ClassSubjects = new HashSet<ClassSubject>();
        }

        public string SubjectId { get; set; } = null!;
        public string SubjectName { get; set; } = null!;

        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
    }
}
