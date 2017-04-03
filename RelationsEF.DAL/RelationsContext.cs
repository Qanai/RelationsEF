using RelationsEF.Domain;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.SqlServer;

namespace RelationsEF.DAL
{
    public class RelationsContext : DbContext
    {
        public RelationsContext()
            : base("name=RelationsEF")
        {
            var ensureDLLIsCopied = SqlProviderServices.Instance;


            //Database.SetInitializer(new MockChangeInitializer());
            Database.SetInitializer(new MockNewInitializer());
            
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .HasMany(up => up.Courses)
                .WithMany(c => c.UserProfiles)
                .Map(mc =>
                {
                    mc.ToTable("UserCourse");
                    mc.MapLeftKey("UserProfileID");
                    mc.MapRightKey("CourseID");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
