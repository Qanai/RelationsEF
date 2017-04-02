using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelationsEF.Web.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public int UserProfileID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AssignedCourseData> Courses { get; set; }
    }
}