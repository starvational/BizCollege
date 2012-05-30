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

        public List<Course> FindCourses(String keyword)
        {
            var allCourses = m_contextCoursesModel.GetAllCourses();
            return (from c in allCourses
                    where c.Name.ToLower().Contains(keyword.ToLower())
                    select c).ToList();
        }

        public void AddSlideToCourse(String courseId, CourseContent content)
        {
            if (String.IsNullOrEmpty(courseId))
            {
                throw new ArgumentException("courseId");
            }
            if (!CourseExists(courseId))
            {
                throw new ArgumentException("course cannot be found!"); 
            }
            
            var targetCourse = m_contextCoursesModel.GetCourse(courseId);
            targetCourse.CourseSlides.Add(content);
            m_contextCoursesModel.AddOrUpdateCourse(targetCourse); 

        }

        public void DeleteSlide(String courseId, String contentId)
        {
            if (String.IsNullOrEmpty(courseId))
            {
                throw new ArgumentException("courseId");
            }
            if (!CourseExists(courseId))
            {
                throw new ArgumentException("course cannot be found!");
            }

            var targetCourse = m_contextCoursesModel.GetCourse(courseId);
            var targetContent = targetCourse.CourseSlides.Where(c => c.Id == contentId).FirstOrDefault();
            targetCourse.CourseSlides.Remove(targetContent); 
            m_contextCoursesModel.AddOrUpdateCourse(targetCourse); 
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