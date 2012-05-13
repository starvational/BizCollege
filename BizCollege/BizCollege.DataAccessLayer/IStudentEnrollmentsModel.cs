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
        StudentRecord AddEnrollment(string username, string courseId);
        void RemoveEnrollment(string username, string courseId);
        StudentRecord GetStudentRecord(string username);
        void SetStudentCourseCompletion(string username, string courseId);
        StudentRecord SetStudentEnrollmentLastViewedSlide(string username, string courseId, int LastViewedSlideIndex);
    }
}
