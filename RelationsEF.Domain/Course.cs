using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RelationsEF.Domain
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseDescription { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
