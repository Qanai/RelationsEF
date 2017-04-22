﻿using RelationsEF.DAL;
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
        private IUnitOfWork unitOfWork;
        private IUserProfileRepository userRepo;
        private ICourseRepository courseRepo;

        public BusinessLayer()
        {
            unitOfWork = new UnitOfWork();
            userRepo = unitOfWork.GetRepository<UserProfileRepository>();
            courseRepo = unitOfWork.GetRepository<CourseRepository>();
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
            userRepo.Add(users);
            await unitOfWork.CommitAsync();
        }

        public async Task UpdateUser(params UserProfile[] users)
        {
            userRepo.Update(users);
            await unitOfWork.CommitAsync();
        }

        public async Task RemoveUser(params UserProfile[] users)
        {
            userRepo.Remove(users);
            await unitOfWork.CommitAsync();
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
            courseRepo.Add(courses);
            await unitOfWork.CommitAsync();
        }

        public async Task UpdateCourse(params Course[] courses)
        {
            courseRepo.Update(courses);
            await unitOfWork.CommitAsync();
        }

        public async Task RemoveCourse(params Course[] courses)
        {
            courseRepo.Remove(courses);
            await unitOfWork.CommitAsync();
        }
        
        #endregion

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
