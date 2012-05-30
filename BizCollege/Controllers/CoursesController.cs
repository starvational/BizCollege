using System;
using System.Web.Mvc;
using BizCollege.DataAccessLayer;
using BizCollege.DataAccessLayer.Domain;
using BizCollegeMvc.Models;
using BizCollegeMvc.Services;
using NHibernate.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace BizCollegeMvc.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {

        private CoursesService m_contextCoursesService;
        private EnrollmentService m_contextEnrollmentService;

        public CoursesController()
        {
            m_contextCoursesService = new CoursesService();
            m_contextEnrollmentService = new EnrollmentService();
        }

        //
        // GET: /Courses/

        public ActionResult Index()
        {
            String contextUserName = User.Identity.Name;
            if (String.IsNullOrEmpty(contextUserName))
            {
                ViewBag.Message = "we couldn't locate your profile";
                return View("Error");
            }
            StudentRecord studentRecord = null;
            List<StudentEnrollmentViewModel> listOfEnrolledCourses = null;
            try
            {
                var enrollmentModel = new StudentEnrollmentsModel();
                studentRecord = enrollmentModel.GetStudentRecord(contextUserName);
                if (studentRecord == null)
                {
                    ViewBag.Message = "our records show that you're not enrolled in any classes..";
                    return View("Error");
                }
                // TO DO: 
                // Find a more elegant way to do this 
                // probably move it to the service. 
                listOfEnrolledCourses = new List<StudentEnrollmentViewModel>();

                foreach (var item in studentRecord.StudentCourseEnrollments)
                {
                    listOfEnrolledCourses.Add(new StudentEnrollmentViewModel
                    {
                        Course = m_contextCoursesService.GetCourse(item.CourseId),
                        EnrollmentInfo = item
                    });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }

            return View(listOfEnrolledCourses);
        }

        //
        // GET: /Courses/Details/DummyClass

        public ActionResult Details(String id)
        {

            if (!m_contextEnrollmentService.IsStudentEnrolled(User.Identity.Name, id))
            {
                ViewBag.Message = "you're not enrolled in this course";
                return View("Error");
            }

            var enrollmentInfo = m_contextEnrollmentService.GetEnrollmentInfo(
                User.Identity.Name, id);
            var courseInfo = m_contextCoursesService.GetCourse(
                enrollmentInfo.CourseId);
            var courseEnrollment = new StudentEnrollmentViewModel
            {
                Course = courseInfo,
                EnrollmentInfo = enrollmentInfo
            };

            return View(courseEnrollment);
        }

        public ActionResult Search()
        {
            return View();
        }
        public ActionResult GetCourses(String keywords)
        {
            var courses = m_contextCoursesService.FindCourses(keywords);
            return View(courses);
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

        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var courseToSubmit = new Course
                {
                    CreatedByUsername = User.Identity.Name,
                    LastUpdateByUsername = User.Identity.Name,
                    CreationDate = DateTime.Now,
                    Description = collection["description"],
                    Name = collection["name"],
                    LastUpdated = DateTime.Now,
                    State = CourseState.Active,
                    NumberOfCourseCompletions = 0
                };


                CoursesModel cm = new CoursesModel();
                var savedCourse = cm.AddOrUpdateCourse(courseToSubmit);

                return RedirectToAction("Create", "CourseContents", new { id = savedCourse.Id });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Courses/Edit/5

        public ActionResult Edit(String id)
        {
            return View();
        }

        //
        // POST: /Courses/Edit/5

        [HttpPost]
        public ActionResult Edit(String id, FormCollection collection)
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
        // GET: /Courses/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Courses/Delete/5

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
        /// Enrolls a user into a certain course. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public ActionResult Enroll(String username, String courseId, bool isJson)
        {
            JsonOperationResult retVal = new JsonOperationResult
            {
                ErrorCode = ErrorCodes.Success
            };

            ActionResult retAction = View(retVal);

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(courseId))
            {
                // return erorr code. invalide data 
                retVal.Message = "Course Id or Username is not valid.";
                retVal.ErrorCode = ErrorCodes.InvalidData;
            }
            try
            {
                m_contextEnrollmentService.EnrollStudent(username, courseId);
                retVal.ErrorCode = ErrorCodes.Success; 
            }
            catch (Exception ex)
            {
                retVal.Message = ex.Message;
                retVal.ErrorCode = ErrorCodes.Fail;
            }


            if (isJson)
            {
                retAction = Json(retVal, JsonRequestBehavior.AllowGet);
            }
           

            return retAction;
        }
    }
}
