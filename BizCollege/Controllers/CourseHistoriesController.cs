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
    public class CourseHistoriesController : Controller
    {
        private BizCollegeItrContext context = new BizCollegeItrContext();

        //
        // GET: /CourseHistories/

        public ViewResult Index()
        {
            return View(context.CourseHistories.Include(coursehistory => coursehistory.Course).ToList());
        }

        //
        // GET: /CourseHistories/Details/5

        public ViewResult Details(int id)
        {
            CourseHistory coursehistory = context.CourseHistories.Single(x => x.Id == id);
            return View(coursehistory);
        }

        //
        // GET: /CourseHistories/Create

        public ActionResult Create()
        {
            ViewBag.PossibleCourses = context.Courses;
            return View();
        } 

        //
        // POST: /CourseHistories/Create

        [HttpPost]
        public ActionResult Create(CourseHistory coursehistory)
        {
            if (ModelState.IsValid)
            {
                context.CourseHistories.Add(coursehistory);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleCourses = context.Courses;
            return View(coursehistory);
        }
        
        //
        // GET: /CourseHistories/Edit/5
 
        public ActionResult Edit(int id)
        {
            CourseHistory coursehistory = context.CourseHistories.Single(x => x.Id == id);
            ViewBag.PossibleCourses = context.Courses;
            return View(coursehistory);
        }

        //
        // POST: /CourseHistories/Edit/5

        [HttpPost]
        public ActionResult Edit(CourseHistory coursehistory)
        {
            if (ModelState.IsValid)
            {
                context.Entry(coursehistory).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleCourses = context.Courses;
            return View(coursehistory);
        }

        //
        // GET: /CourseHistories/Delete/5
 
        public ActionResult Delete(int id)
        {
            CourseHistory coursehistory = context.CourseHistories.Single(x => x.Id == id);
            return View(coursehistory);
        }

        //
        // POST: /CourseHistories/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseHistory coursehistory = context.CourseHistories.Single(x => x.Id == id);
            context.CourseHistories.Remove(coursehistory);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}