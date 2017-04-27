﻿using RelationsEF.Domain;
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
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            var ensureDLLIsCopied = SqlProviderServices.Instance;

            Database.SetInitializer(new MockNewInitializer());
            //Database.SetInitializer(new MockChangeInitializer());
            //Database.SetInitializer<RelationsContext>(null);
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
