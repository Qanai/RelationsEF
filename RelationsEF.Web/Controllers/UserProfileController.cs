using RelationsEF.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RelationsEF.Web.Models.ViewModels;
using RelationsEF.Domain;

namespace RelationsEF.Web.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly RelationsContext db = new RelationsContext();

        //
        // GET: /UserProfile/

        public ActionResult Index()
        {
            var userProfiles = db.UserProfiles.ToList();
            var userProfileModels = userProfiles.Select(up => up.ToViewModel()).ToList();

            return View(userProfileModels);
        }

        public ActionResult Create()
        {
            var userProfileViewModel = new UserProfileViewModel { };

            return View(userProfileViewModel);
        }

        [HttpPost]
        public ActionResult Create(UserProfileViewModel userProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                var userProfile = new UserProfile { Name = userProfileViewModel.Name };

                AddOrUpdateCourses(userProfile, userProfileViewModel.Courses);
                db.UserProfiles.Add(userProfile);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(userProfileViewModel);
        }

        private void AddOrUpdateCourses(UserProfile userProfile, IEnumerable<AssignedCourseData> assignedCourses)
        {
            if (assignedCourses != null)
            {
                foreach (var assignedCourse in assignedCourses)
                {
                    if (assignedCourse.Assigned)
                    {
                        var course = new Course { CourseID = assignedCourse.CourseID };
                        db.Courses.Attach(course);
                        userProfile.Courses.Add(course);
                    }
                }
            }
        }
    }
}
