using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PSExcercise.DAL;

namespace PSExcercise.CustomFilter
{
    public class EmployeeChecker
    {
        private VersityContext db = new VersityContext();
        public bool Login(string username,string password)
        {
            return (db.Users.Any(user => user.Name.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password));
            
        }
    }
}