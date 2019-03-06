using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PSExcercise.DAL;
using PSExcercise.Models;
using System.Threading;
using PSExcercise.CustomFilter;

namespace PSExcercise.Controllers
{
    public class EmployeeController : ApiController
    {
        private VersityContext db = new VersityContext();

        [BasicAuthenticationFilter]
        public HttpResponseMessage Get(string gender="all")
        {
            var user = Thread.CurrentPrincipal.Identity.Name;
            
            switch (user.ToLower())
            {
                case "male":
                    return Request.CreateResponse(HttpStatusCode.OK, db.Employees.Where(e => e.Gender.ToLower() == "male"));
                case "female":
                    return Request.CreateResponse(HttpStatusCode.OK, db.Employees.Where(e => e.Gender.ToLower() == "female"));
                default:
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid User");
            }
            
        }

        public HttpResponseMessage Get(int id)
        {
            var em = db.Employees.FirstOrDefault(e => e.Id == id);
            if (em != null)
            {
                return Request.CreateResponse(HttpStatusCode.Found, em);
            }
            else
            return Request.CreateResponse(HttpStatusCode.BadRequest,"Employee with id="+id+" is not found");
        }

        public HttpResponseMessage Post(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                var message = Request.CreateResponse(HttpStatusCode.Created,employee);
                message.Headers.Location = new Uri(Request.RequestUri+"/" + employee.Id.ToString());
                return message;

            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            
        }

        public HttpResponseMessage Delete(int id)
        {
            var em = db.Employees.FirstOrDefault(e => e.Id == id);
            if (em != null)
            {
                db.Employees.Remove(em);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, em);
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Employee with " + id + " is not exist");
        }

        public HttpResponseMessage Put(int id, Employee employee)
        {
            var em = db.Employees.FirstOrDefault(e => e.Id == id);
            if (ModelState.IsValid)
            {
                if (em != null)
                {
                    em.Name = employee.Name;
                    em.Age = employee.Age;
                    em.Gender = employee.Gender;
                    em.Salary = employee.Salary;
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, em);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Employee with id = " + id + " does not exist");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }
    }
}
