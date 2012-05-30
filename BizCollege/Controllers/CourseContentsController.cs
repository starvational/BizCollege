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

        public ActionResult Details(String id)
        {
            if (!m_contextCoursesService.CourseExists(id))
            {
                ViewBag.Message = "we couldn't locate the requested course. ";
                return View("Error");
            }

            var targetCourse = m_contextCoursesService.GetCourse(id);
            TempData["courseId"] = id;
            if (targetCourse == null)
            {
                ViewBag.Message = "the requested course doesn't have any slides.";
                return View("Error");
            }
            return View(targetCourse.CourseSlides);
        }


        //
        // GET: /CourseContents/Create

        public ActionResult Create(String courseId)
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
                var contentToSubmit = new CourseContent
                {
                    CourseContentType = (ContentType)Enum.Parse(typeof(ContentType), collection["CourseContentType"], true),
                    Description = collection["description"],
                    IndexInSquence = Int32.Parse(collection["IndexInSquence"]),
                    ResourcePath = collection["ResourcePath"],
                    Title = collection["Title"]
                };
                m_contextCoursesService.AddSlideToCourse(collection["courseId"], contentToSubmit);
                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }

        //
        // GET: /CourseContents/Edit/5

        public ActionResult Edit(String id, String courseId)
        {
            if (!m_contextCoursesService.CourseExists(courseId))
            {
                ViewBag.Message = "course couldn't be found.";
                return View("Error");
            }

            var course = m_contextCoursesService.GetCourse(courseId);
            if (courseId == null)
            {
                ViewBag.Message = "course couldn't be found.";
                return View("Error");
            }
            var targetSlide = course.CourseSlides.Where(s => s.Id == id).FirstOrDefault();
            return View(targetSlide);
        }

        //
        // POST: /CourseContents/Edit/5

        [HttpPost]
        public ActionResult Edit(String id, String courseId, FormCollection collection)
        {
            try
            {
                // TO DO: 
                // ADD THE EDIT LOGIC.  

                var contentToSubmit = new CourseContent
               {
                   CourseContentType = (ContentType)Enum.Parse(typeof(ContentType), collection["CourseContentType"], true),
                   Description = collection["description"],
                   IndexInSquence = Int32.Parse(collection["IndexInSquence"]),
                   ResourcePath = collection["ResourcePath"],
                   Title = collection["Title"]
               };
                m_contextCoursesService.AddSlideToCourse(courseId, contentToSubmit);


                return Redirect(Request.UrlReferrer.AbsoluteUri.ToString());


            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /CourseContents/Delete/5

        public ActionResult Delete(string id)
        {


            return View();
        }

        //
        // POST: /CourseContents/Delete/5

        [HttpPost]
        public ActionResult Delete(String id, String courseId, FormCollection collection)
        {
            try
            {
                m_contextCoursesService.DeleteSlide(courseId, id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddSlide(FormCollection collection)
        {

            return null;
        }
        /// <summary>
        /// Get JSON only 
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public ActionResult ContentStats(String courseId)
        {
            var course = m_contextCoursesService.GetCourse(courseId);
            if (course != null)
            {
                return Json(new
                {
                    NumberOfSlides = course.CourseSlides.Count,
                    NumberOfCompletions = course.NumberOfCourseCompletions
                },
                JsonRequestBehavior.AllowGet);
            }
            return Json(String.Empty, JsonRequestBehavior.AllowGet);
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
                    slideIndex);
                if (targetSlide == null)
                {
                    throw new Exception("this class doesn't have any slides, yet!");
                }
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
