using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelationsEF.Web.Models.ViewModels
{
    public class CourseViewModel
    {
        public int CourseID { get; set; }
        public string CourseDescription { get; set; }
        public virtual ICollection<UserProfileViewModel> UserProfiles { get; set; }
    }
}