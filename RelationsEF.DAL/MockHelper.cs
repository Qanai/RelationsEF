using RelationsEF.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationsEF.DAL
{
    public static class MockHelper
    {
        public static void Seed(RelationsContext context)
        {
            if (context.Courses.Count() == 0)
            {
                var course1 = new Course { CourseID = 1, CourseDescription = "Bird Watching" };
                var course2 = new Course { CourseID = 2, CourseDescription = "Basket weaving for beginners" };
                var course3 = new Course { CourseID = 3, CourseDescription = "Photography 101" };

                context.Courses.Add(course1);
                context.Courses.Add(course2);
                context.Courses.Add(course3);
            }
        }
    }
}
