namespace PSExcercise.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using PSExcercise.Models;
    using System.Collections.Generic;
    using PSExcercise.DAL;

    internal sealed class Configuration : DbMigrationsConfiguration<VersityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "PSExcercise.DAL.VersityContext";
        }

        protected override void Seed(VersityContext context)
        {
            var students = new List<Student>
                {
                new Student { FirstName = "Carson", LastName = "Alexander",EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { FirstName = "Meredith", LastName = "Alonso",EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Arturo", LastName = "Anand",EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstName = "Gytis", LastName = "Barzdukas",EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Yan", LastName = "Li",EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Peggy", LastName = "Justice",EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { FirstName = "Laura", LastName = "Norman",EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstName = "Nino", LastName = "Olivetto",EnrollmentDate = DateTime.Parse("2005-09-01") }
                };
            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var instructors = new List<Instructor>
                {
                new Instructor { FirstName = "Kim", LastName = "Abercrombie",HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstName = "Fadi", LastName = "Fakhouri",HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstName = "Roger", LastName = "Harui",HireDate = DateTime.Parse("1998-07-01") },
                new Instructor { FirstName = "Candace", LastName = "Kapoor",HireDate = DateTime.Parse("2001-01-15") },
                new Instructor { FirstName = "Roger", LastName = "Zheng",HireDate = DateTime.Parse("2004-02-12") }
                };
            instructors.ForEach(s => context.Instructors.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var departments = new List<Department>
                {
                new Department { Name = "English", Budget = 350000,StartDate = DateTime.Parse("2007-09-01"),
                InstructorId = instructors.Single( i => i.LastName == "Abercrombie").InstructorId },
                new Department { Name = "Mathematics", Budget = 100000,StartDate = DateTime.Parse("2007-09-01"),
                InstructorId = instructors.Single( i => i.LastName == "Fakhouri").InstructorId },
                new Department { Name = "Engineering", Budget = 350000,StartDate = DateTime.Parse("2007-09-01"),
                InstructorId = instructors.Single( i => i.LastName == "Harui").InstructorId },
                new Department { Name = "Economics", Budget = 100000,StartDate = DateTime.Parse("2007-09-01"),
                InstructorId = instructors.Single( i => i.LastName == "Kapoor").InstructorId }
                };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var courses = new List<Course>
                {
                new Course {CourseId = 1050, CourseTitle = "Chemistry", Credit = 3,
                DepartmentId = departments.Single( s => s.Name == "Engineering").DepartmentId,
                Instructors = new List<Instructor>()
                },
                new Course {CourseId = 4022, CourseTitle = "Microeconomics", Credit = 3,
                DepartmentId = departments.Single( s => s.Name == "Economics").DepartmentId,
                Instructors = new List<Instructor>()
                },
                new Course {CourseId = 4041, CourseTitle = "Macroeconomics", Credit = 3,
                DepartmentId = departments.Single( s => s.Name == "Economics").DepartmentId,
                Instructors = new List<Instructor>()
                },
                new Course {CourseId = 1045, CourseTitle = "Calculus", Credit = 4,
                DepartmentId = departments.Single( s => s.Name == "Mathematics").DepartmentId,
                Instructors = new List<Instructor>()
                },
                new Course {CourseId = 3141, CourseTitle = "Trigonometry", Credit = 4,
                DepartmentId = departments.Single( s => s.Name == "Mathematics").DepartmentId,
                Instructors = new List<Instructor>()
                },
                new Course {CourseId = 2021, CourseTitle = "Composition", Credit = 3,
                DepartmentId = departments.Single( s => s.Name == "English").DepartmentId,
                Instructors = new List<Instructor>()
                },
                new Course {CourseId = 2042, CourseTitle = "Literature", Credit = 4,
                DepartmentId = departments.Single( s => s.Name == "English").DepartmentId,
                Instructors = new List<Instructor>()
                },
                };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseId, s));
            context.SaveChanges();

            var officeAssignments = new List<OfficeAssignment>
                {
                new OfficeAssignment {
                InstructorId = instructors.Single( i => i.LastName == "Fakhouri").InstructorId,
                Location = "Smith 17" },
                new OfficeAssignment {
                InstructorId = instructors.Single( i => i.LastName == "Harui").InstructorId,
                Location = "Gowan 27" },
                new OfficeAssignment {
                InstructorId = instructors.Single( i => i.LastName == "Kapoor").InstructorId,
                Location = "Thompson 304" },
                };
            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.InstructorId, s));
            context.SaveChanges();

            AddOrUpdateInstructor(context, "Chemistry", "Kapoor");
            AddOrUpdateInstructor(context, "Chemistry", "Harui");
            AddOrUpdateInstructor(context, "Microeconomics", "Zheng");
            AddOrUpdateInstructor(context, "Macroeconomics", "Zheng");
            AddOrUpdateInstructor(context, "Calculus", "Fakhouri");
            AddOrUpdateInstructor(context, "Trigonometry", "Harui");
            AddOrUpdateInstructor(context, "Composition", "Abercrombie");
            AddOrUpdateInstructor(context, "Literature", "Abercrombie");
            AddOrUpdateInstructor(context, "Literature", "Abercrombie");
            context.SaveChanges();

            var enrollments = new List<Enrollment>
                {
                new Enrollment {
                StudentId = students.Single(s => s.LastName == "Alexander").StudentId,
                CourseId = courses.Single(c => c.CourseTitle == "Chemistry" ).CourseId,
                Grade = Grade.A
                },
                new Enrollment {
                StudentId = students.Single(s => s.LastName == "Alexander").StudentId,
                CourseId = courses.Single(c => c.CourseTitle == "Microeconomics" ).CourseId,
                Grade = Grade.C
                },
                new Enrollment {
                StudentId = students.Single(s => s.LastName == "Alexander").StudentId,
                CourseId = courses.Single(c => c.CourseTitle == "Macroeconomics" ).CourseId,
                Grade = Grade.B
                },
                new Enrollment {
                StudentId = students.Single(s => s.LastName == "Alonso").StudentId,
                CourseId = courses.Single(c => c.CourseTitle == "Calculus" ).CourseId,
                Grade = Grade.B
                },
                new Enrollment {
                StudentId = students.Single(s => s.LastName == "Alonso").StudentId,
                CourseId = courses.Single(c => c.CourseTitle == "Trigonometry" ).CourseId,
                Grade = Grade.B
                },
                new Enrollment {
                StudentId = students.Single(s => s.LastName == "Alonso").StudentId,
                CourseId = courses.Single(c => c.CourseTitle == "Composition" ).CourseId,
                Grade = Grade.B
                },
                new Enrollment {
                StudentId = students.Single(s => s.LastName == "Anand").StudentId,
                CourseId = courses.Single(c => c.CourseTitle == "Chemistry" ).CourseId
                },
                new Enrollment {
                StudentId = students.Single(s => s.LastName == "Anand").StudentId,
                CourseId = courses.Single(c => c.CourseTitle == "Microeconomics").CourseId,
                Grade = Grade.B
                },
                new Enrollment {
                StudentId = students.Single(s => s.LastName == "Barzdukas").StudentId,
                CourseId = courses.Single(c => c.CourseTitle == "Chemistry").CourseId,
                Grade = Grade.B
                },
                new Enrollment {
                StudentId = students.Single(s => s.LastName == "Li").StudentId,
                CourseId = courses.Single(c => c.CourseTitle == "Composition").CourseId,
                Grade = Grade.B
                },
                new Enrollment {
                StudentId = students.Single(s => s.LastName == "Justice").StudentId,
                CourseId = courses.Single(c => c.CourseTitle == "Literature").CourseId,
                Grade = Grade.B
                }
                };
            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                s =>
                s.Student.StudentId == e.StudentId &&
                s.Course.CourseId == e.CourseId).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }

        void AddOrUpdateInstructor(VersityContext context, string courseTitle, string instructorName)
        {
            var crs = context.Courses.SingleOrDefault(c => c.CourseTitle == courseTitle);
            var inst = crs.Instructors.SingleOrDefault(i => i.LastName == instructorName);
            if (inst == null)
                crs.Instructors.Add(context.Instructors.Single(i => i.LastName == instructorName));
        }
    }
}
