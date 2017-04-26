using RelationsEF.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationsEF.BL
{
    public interface IBusinessLayer : IDisposable
    {
        #region UserProfile
        IList<UserProfile> GetAllUsers();
        IList<UserProfile> GetCourseUsers(int courseId);
        UserProfile GetUser(int id);
        Task AddUser(params UserProfile[] users);
        Task UpdateUser(params UserProfile[] users);
        Task RemoveUser(params UserProfile[] users);
        Task UpdateUserCourses(int userId, params Course[] courses);
        #endregion

        #region Course
        IList<Course> GetAllCourses();
        IList<Course> GetUserCourses(int userId);
        Course GetCourse(int id);
        Task AddCourse(params Course[] courses);
        Task UpdateCourse(params Course[] courses);
        Task RemoveCourse(params Course[] courses);
        #endregion
    }
}
