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
        private CoursesModel m_contextCoursesModel;

        public CoursesService()
        {
            m_contextCoursesModel = new CoursesModel(); 
        } 

        public bool CourseExists(String courseId)
        {
            return m_contextCoursesModel.GetCourse(courseId) != null; 
        }

        public Course GetCourse(String courseId)
        {
            return m_contextCoursesModel.GetCourse(courseId);
        }

        public CourseContent GetCourseSlide(String courseId, int index)
        {
            if (String.IsNullOrEmpty(courseId))
            {
                throw new ArgumentException("courseId");
            }
            CourseContent retVal = null;
            var targetCourse = m_contextCoursesModel.GetCourse(courseId);
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