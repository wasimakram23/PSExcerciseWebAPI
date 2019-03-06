using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PSExcercise.Models
{
    public enum Grade
    {
        A,B,C,D,E,F
    }
    public class Enrollment
    {
        public int Id { get; set; }

        [DisplayFormat(NullDisplayText ="No Grade")]
        public Grade? Grade { get; set; }

        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public virtual Student Student { get; set;}
        public virtual Course Course { get; set; }
    }
}