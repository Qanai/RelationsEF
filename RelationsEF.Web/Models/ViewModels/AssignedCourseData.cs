using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelationsEF.Web.Models.ViewModels
{
    public class AssignedCourseData
    {
        public int CourseID { get; set; }
        public string CourseDescription { get; set; }
        public bool Assigned { get; set; }
    }
}