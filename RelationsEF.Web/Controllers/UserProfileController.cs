using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RelationsEF.Web.Models.ViewModels;
using RelationsEF.Domain;
using RelationsEF.BL;
using System.Threading.Tasks;

namespace RelationsEF.Web.Controllers
{
    public class UserProfileController : Controller
    {
        private IBusinessLayer bl = new BusinessLayer();

        //
        // GET: /UserProfile/

        public ActionResult Index()
        {
            var userProfiles = bl.GetAllUsers();
            var userProfileModels = userProfiles.Select(up => up.ToViewModel()).ToList();

            return View(userProfileModels);
        }

        public ActionResult Create()
        {
            var userProfileViewModel = new UserProfileViewModel
            {
                Courses = PopulateCourseData()
            };

            return View(userProfileViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserProfileViewModel userProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                var userProfile = new UserProfile { Name = userProfileViewModel.Name };

                await bl.AddUser(userProfile);
                int newUserId = userProfile.UserProfileID;
                userProfile = bl.GetUser(newUserId);
                if (userProfile != null)
                {
                    AddOrUpdateCourses(userProfile, userProfileViewModel.Courses);
                    await bl.UpdateUser(userProfile);
                    //await bl.UpdateCourse(userProfile.Courses.ToArray());
                }

                return RedirectToAction("Index");
            }

            return View(userProfileViewModel);
        }

        private void AddOrUpdateCourses(UserProfile userProfile, IEnumerable<AssignedCourseData> assignedCourses)
        {
            if (assignedCourses != null)
            {
                var courses = bl.GetAllCourses().Where(c => assignedCourses.Any(ac => ac.Assigned && ac.CourseID == c.CourseID));
                bl.UpdateUserCourses(userProfile.UserProfileID, courses.ToArray());
                

                //foreach (var assignedCourse in assignedCourses)
                //{
                //    if (assignedCourse.Assigned)
                //    {
                //        var course = bl.GetCourse(assignedCourse.CourseID); //new Course { CourseID = assignedCourse.CourseID };
                //        //db.Courses.Attach(course);
                //        userProfile.Courses.Add(course);
                //        course.UserProfiles.Add(userProfile);
                //        //bl.UpdateCourse(course);
                //    }
                //}
            }
        }

        private ICollection<AssignedCourseData> PopulateCourseData()
        {
            var courses = bl.GetAllCourses();
            var assignedCourses = new List<AssignedCourseData>();

            foreach (var item in courses)
            {
                assignedCourses.Add(new AssignedCourseData
                {
                    CourseID = item.CourseID,
                    CourseDescription = item.CourseDescription,
                    Assigned = false
                });
            }

            return assignedCourses;
        }

        protected override void Dispose(bool disposing)
        {
            bl.Dispose();
            base.Dispose(disposing);
        }
    }
}
