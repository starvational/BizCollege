using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizCollege.DataAccessLayer;
using BizCollege.DataAccessLayer.Domain;

namespace BizCollegeMvc.Services
{
    public class CoursesService
    {
        private CoursesModel _contextCoursesModel;

        public CoursesService()
        {
            _contextCoursesModel = new CoursesModel(); 
        } 

        public bool CourseExists(String courseId)
        {
            return _contextCoursesModel.GetCourse(courseId) != null; 
        }

        public Course GetCourse(String courseId)
        {
            return _contextCoursesModel.GetCourse(courseId);
        }

        public CourseContent GetCourseSlide(String courseId, int index)
        {
            if (String.IsNullOrEmpty(courseId))
                throw new ArgumentException("courseId");
            CourseContent retVal = null;
            var targetCourse = _contextCoursesModel.GetCourse(courseId);
            if (targetCourse.CourseSlides != null)
            {
                retVal = targetCourse.CourseSlides
                    .Where(s => s.IndexInSquence == index)
                    .FirstOrDefault();
            }
            return retVal;
        }
    }
}