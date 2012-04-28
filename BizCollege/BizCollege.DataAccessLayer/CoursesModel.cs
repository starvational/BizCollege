using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizCollege.DataAccessLayer.Repository;
using BizCollege.DataAccessLayer.Domain;

namespace BizCollege.DataAccessLayer
{
    /// <summary>
    /// Used to interface with the BizCollege courses repository: to 
    /// add new courses to the data store, remove a course, get 
    /// a course container or get a list of all courses in the system.
    /// </summary>
    public class CoursesModel : ICoursesModel
    {
        private IRepository<Course, string> m_repo;

        public CoursesModel()
        {
            m_repo = new BizCollegeRepository<Course, string>();
        }

        public Course AddOrUpdateCourse(Domain.Course newcourse)
        {
            return m_repo.AddOrUpdate(newcourse);
        }

        public void RemoveCourse(string courseId)
        {
            m_repo.Remove(courseId);
        }

        public Domain.Course GetCourse(string courseId)
        {
            return m_repo.Get(courseId);
        }

        public ICollection<Domain.Course> GetAllCourses()
        {
            return m_repo.GetAllItems();
        }
    }
}
