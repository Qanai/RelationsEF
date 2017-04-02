using RelationsEF.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationsEF.DAL
{
    class MockInitializer : DropCreateDatabaseIfModelChanges<RelationsContext>
    {
        protected override void Seed(RelationsContext context)
        {
            base.Seed(context);

            var course1 = new Course { CourseID = 1, CourseDescription = "Bird Watching" };
            var course2 = new Course { CourseID = 2, CourseDescription = "Basket weaving for beginners" };
            var course3 = new Course { CourseID = 3, CourseDescription = "Photography 101" };

            context.Courses.Add(course1);
            context.Courses.Add(course2);
            context.Courses.Add(course3);
        }
    }
}
