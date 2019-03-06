using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PSExcercise.Models
{
    public class User
    {
        public int id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}