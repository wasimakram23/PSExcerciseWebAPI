using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PSExcercise.Models;

namespace PSExcercise.DataModel
{
    public class Enroll
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        
    }
    public class StudentInfo
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<Enroll> enroll { get; set; }
        public StudentInfo()
        {
            enroll = new List<Enroll>();
        }

    }
}