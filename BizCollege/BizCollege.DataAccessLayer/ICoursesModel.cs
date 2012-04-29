using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizCollege.DataAccessLayer.Domain;

namespace BizCollege.DataAccessLayer
{
    /// <summary>
    /// Interface to the BizCollege courses repository
    /// </summary>
    public interface ICoursesModel
    {
        Course AddOrUpdateCourse(Course courseToAddOrUpdate);
        void RemoveCourse(string courseId);
        Course GetCourse(string courseId);
        ICollection<Course> GetAllCourses();
    }
}
