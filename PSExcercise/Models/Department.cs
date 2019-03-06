using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PSExcercise.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        public int? InstructorId { get; set; }
        public virtual Instructor Administrator { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}