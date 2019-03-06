using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PSExcercise.DAL;
using PSExcercise.Models;
using PSExcercise.DataModel;

namespace PSExcercise.Controllers
{
    public class StudentsController : ApiController
    {
        private VersityContext db = new VersityContext();

        [Route("api/Students")]
        // GET: api/Students
        public object Get()
        {
            List<StudentInfo> list = new List<StudentInfo>();
            var result = db.Students;
            foreach(var student in result)
            {
                var info = new StudentInfo();
                info.Name = student.FirstName + " " + student.LastName;
                info.Date = student.EnrollmentDate;
                foreach(var item in student.Enrollments)
                {
                    info.enroll.Add(new Enroll { CourseId = item.Course.CourseId, CourseName = item.Course.CourseTitle });
                }
                list.Add(info);
            }
            return list;
        }

      
        // GET: api/Students/5
        [Route("api/Students/{id}",Name ="GetStudentById")]
        public object GetStudent(int id)
        {
            var info = new StudentInfo();
            var enrollinfo = new Enroll();
            var result = db.Students.Find(id);
            info.Name = result.FirstName + " " + result.LastName;
            info.Date = result.EnrollmentDate;
            foreach(var item in result.Enrollments)
            {
                info.enroll.Add(new Enroll { CourseId = item.Course.CourseId, CourseName = item.Course.CourseTitle });
            }
            return info;
        }

        // PUT: api/Students/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Student(int id, Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.StudentId)
            {
                return BadRequest();
            }

            db.Entry(student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Students
        [ResponseType(typeof(Student))]
        [Route("api/Students")]
        public HttpResponseMessage PostStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            db.Students.Add(student);
            db.SaveChanges();
            string uri = Url.Link("GetStudentById", new { id = student.StudentId });
            var response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(uri);
            return response;
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult DeleteStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            db.SaveChanges();

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return db.Students.Count(e => e.StudentId == id) > 0;
        }
    }
}