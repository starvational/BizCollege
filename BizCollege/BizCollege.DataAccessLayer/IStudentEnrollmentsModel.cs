using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizCollege.DataAccessLayer.Domain;

namespace BizCollege.DataAccessLayer
{
    /// <summary>
    /// Interface to the BizCollge student enrollments repository
    /// </summary>
    public interface IStudentEnrollmentsModel
    {
        void EnrollStudentInCourse(string username, string courseId);
        void SetStudentCourseCompletion(string username, string courseId);
        StudentRecord GetStudentEnrollmentInfo(string username);
    }
}
