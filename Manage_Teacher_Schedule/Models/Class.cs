using System;
using System.Collections.Generic;

namespace Manage_Teacher_Schedule.Models
{
    public partial class Class
    {
        public Class()
        {
            ClassSubjects = new HashSet<ClassSubject>();
        }

        public string ClassId { get; set; } = null!;
        public string ClassName { get; set; } = null!;

        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
    }
}
