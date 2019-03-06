using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSExcercise.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int Credit { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}