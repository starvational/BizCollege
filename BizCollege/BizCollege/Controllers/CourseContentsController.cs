using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BizCollege.DataAccessLayer;
using BizCollege.DataAccessLayer.Domain;
using BizCollegeMvc.Services;

namespace BizCollegeMvc.Controllers
{
    public class CourseContentsController : Controller
    {
        private EnrollmentService m_contextEnrollmentService; 
        private CoursesService m_contextCoursesService;

        public CourseContentsController()
        {
            m_contextEnrollmentService = new EnrollmentService();
            m_contextCoursesService = new CoursesService(); 
        }
        //
        // GET: /CourseContents/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /CourseContents/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CourseContents/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /CourseContents/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /CourseContents/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /CourseContents/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /CourseContents/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /CourseContents/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// GetSlide
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="slideIndex"></param>
        /// <returns></returns>
        public ActionResult GetSlide(String courseId, int slideIndex)
        {
            String viewName = "Error";
            CourseContent targetSlide = new CourseContent(); //empty
            try
            {
                var enrollmentInfo = m_contextEnrollmentService.GetEnrollmentInfo(
                    User.Identity.Name, courseId);
                targetSlide = m_contextCoursesService.GetCourseSlide(
                    courseId,
                    enrollmentInfo.LastViewedSlideIndex);

                switch (targetSlide.CourseContentType)
                {
                    case ContentType.Audio:
                        viewName = "AudioContent";
                        break;
                    case ContentType.Video:
                        viewName = "VideoContent";
                        break;
                    case ContentType.Document:
                        viewName = "DocumentContent";
                        break;
                    case ContentType.Presentation:
                        viewName = "PresentationContent";
                        break;
                    case ContentType.Interactive:
                        viewName = "InteractiveContent";
                        break;
                    case ContentType.Unknown:
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }

            return View(viewName, targetSlide);
        }
    }
}
