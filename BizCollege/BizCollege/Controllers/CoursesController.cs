using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BizCollegeItr.Models;

namespace BizCollegeItr.Controllers
{   
    public class CoursesController : Controller
    {
        private BizCollegeItrContext context = new BizCollegeItrContext();

        //
        // GET: /Courses/

        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                // get course fors logged-in user.. 
                // right now, we'll return dummy data.  
                return View("Index", GetDummyCourses());

            }
            else 
            {
                return RedirectToAction("LogOn", "Account");
            }
        }

        //
        // GET: /Courses/Details/5

        public ViewResult Details(int id)
        {
            Course course = context.Courses.Single(x => x.CourseId == id);
            return View(course);
        }

        //
        // GET: /Courses/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Courses/Create

        [HttpPost]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                context.Courses.Add(course);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(course);
        }
        
        //
        // GET: /Courses/Edit/5
 
        public ActionResult Edit(int id)
        {
            Course course = context.Courses.Single(x => x.CourseId == id);
            return View(course);
        }

        //
        // POST: /Courses/Edit/5

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                context.Entry(course).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        //
        // GET: /Courses/Delete/5
 
        public ActionResult Delete(int id)
        {
            Course course = context.Courses.Single(x => x.CourseId == id);
            return View(course);
        }

        //
        // POST: /Courses/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = context.Courses.Single(x => x.CourseId == id);
            context.Courses.Remove(course);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        #region [ Private Methods ]  
        private List<Course> GetListOfCourses()
        {
            return new List<Course> 
            { 
                 new Course{Title = "Business 101", Description = "Introduction to business.", DateCreated = DateTime.Now.Subtract(TimeSpan.FromDays(20)), CourseId = 1}, 
                 new Course{Title = "Accounting 101", Description = "Introduction to accounting.", DateCreated = DateTime.Now.Subtract(TimeSpan.FromDays(20)), CourseId = 2},  
                 new Course{Title = "Finance 101", Description = "Introduction to finance.", DateCreated = DateTime.Now.Subtract(TimeSpan.FromDays(20)), CourseId = 3},  
                 new Course{Title = "Tax Regulation ", Description = "Introduction to tax regulations.", DateCreated = DateTime.Now.Subtract(TimeSpan.FromDays(20)), CourseId = 4},  
                 new Course{Title = "Business Law ", Description = "Introduction to business law.", DateCreated = DateTime.Now.Subtract(TimeSpan.FromDays(20)), CourseId = 5}, 
                 new Course{Title = "Business Etiquette", Description = "Introduction to business law.", DateCreated = DateTime.Now.Subtract(TimeSpan.FromDays(20)), CourseId = 6}
            }; 
        }
        private List<CourseHistory> GetDummyCourses()
        {
            var courses = GetListOfCourses(); 
            return new List<CourseHistory> 
            { 
                new CourseHistory
                {
                    Course = courses[0], 
                    CourseId = courses[4].CourseId, 
                    CourseStatus = (int)CourseCompletionStatus.Completed, 
                    StartedOn = DateTime.Now.Subtract(TimeSpan.FromDays(3))
                }, 
                new CourseHistory
                {
                    Course = courses[2], 
                    CourseId = courses[4].CourseId, 
                    CourseStatus = (int)CourseCompletionStatus.NotStarted, 
                    StartedOn = DateTime.Now.Subtract(TimeSpan.FromDays(5))
                },
                 new CourseHistory
                {
                    Course = courses[4], 
                    CourseId = courses[4].CourseId, 
                    CourseStatus = (int)CourseCompletionStatus.Started, 
                    StartedOn = DateTime.Now.Subtract(TimeSpan.FromDays(1))
                },
 
            };
        }
        #endregion
    }
}