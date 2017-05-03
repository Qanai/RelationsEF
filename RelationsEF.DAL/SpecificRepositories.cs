using RelationsEF.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationsEF.DAL
{
    #region Interfaces
    public interface ICourseRepository : IGenericDataRepository<Course> { }
    public interface IUserProfileRepository : IGenericDataRepository<UserProfile> { }
    #endregion

    #region Classes
    public class CourseRepository : GenericDataRepository<Course>, ICourseRepository
    {
        //public CourseRepository()
        //    : this(new RelationsContext()) { }

        public CourseRepository(DbContext context)
            : base(context) { }
    }
    public class UserProfileRepository : GenericDataRepository<UserProfile>, IUserProfileRepository
    {
        //public UserProfileRepository() : this(new RelationsContext()) { }

        public UserProfileRepository(DbContext context) : base(context) { }
    }
    #endregion
}
