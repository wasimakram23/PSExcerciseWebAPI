using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;
using PSExcercise.Models;


namespace PSExcercise.DAL
{
    public class VersityContext:DbContext
    {
     public VersityContext() : base("VersityContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Course>()
                .HasMany(i => i.Instructors).WithMany(c => c.Courses)
                .Map(m=>m.MapLeftKey("CourseId")
                    .MapRightKey("InstructorId")
                    .ToTable("CourseInstructor"));    

        }
    }
}