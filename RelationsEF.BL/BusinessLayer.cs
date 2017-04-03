using RelationsEF.DAL;
using RelationsEF.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationsEF.BL
{
    public class BusinessLayer : IBusinessLayer
    {
        private IUserProfileRepository userRepo;
        private ICourseRepository courseRepo;

        public BusinessLayer()
        {
            userRepo = new UserProfileRepository();
            courseRepo = new CourseRepository();
        }

        #region UserProfile

        public IList<UserProfile> GetAllUsers()
        {
            return userRepo.GetAll();
        }

        public IList<UserProfile> GetCourseUsers(int courseId)
        {
            return null;
        }

        public UserProfile GetUser(int id)
        {
            return userRepo.GetSingle(u => u.UserProfileID == id, u => u.Courses);
        }

        public async Task AddUser(params UserProfile[] users)
        {
            await userRepo.Add(users);
        }

        public async Task UpdateUser(params UserProfile[] users)
        {
            await userRepo.Update(users);
        }

        public async Task RemoveUser(params UserProfile[] users)
        {
            await userRepo.Remove(users);
        }
                
        #endregion

        #region Course
        
        public IList<Course> GetAllCourses()
        {
            return courseRepo.GetAll();
        }

        public IList<Course> GetUserCourses(int userId)
        {
            throw new NotImplementedException();
        }

        public Course GetCourse(int id)
        {
            return courseRepo.GetSingle(c => c.CourseID == id, c => c.UserProfiles);
        }

        public async Task AddCourse(params Course[] courses)
        {
            await courseRepo.Add(courses);
        }

        public async Task UpdateCourse(params Course[] courses)
        {
            await courseRepo.Update(courses);
        }

        public async Task RemoveCourse(params Course[] courses)
        {
            await courseRepo.Remove(courses);
        }
        
        #endregion
    }
}
