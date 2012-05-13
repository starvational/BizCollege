using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizCollege.DataAccessLayer;
using BizCollege.DataAccessLayer.Domain;
using BizCollegeMvc.Models;

namespace BizCollegeMvc.Services
{
    public class EnrollmentService
    {
        private StudentEnrollmentsModel _contextEnrollment; 
        public EnrollmentService()
        {
            _contextEnrollment = new StudentEnrollmentsModel(); 
        }

        public bool IsStudentEnrolled(String username, String courseId)
        {
            var studentRecord = GetStudentRecord(username);
            return studentRecord.StudentCourseEnrollments != null &&
                studentRecord.StudentCourseEnrollments.Where(e => e.CourseId == courseId).Any();
        }

        private StudentRecord GetStudentRecord(String username)
        {  
            if(String.IsNullOrEmpty(username))
                throw new InvalidOperationException("Student cannot be found..");
            return _contextEnrollment.GetStudentRecord(username);  
        }

        public void EnrollStudent(String username, String courseId)
        {
            _contextEnrollment.AddEnrollment(username, courseId); 
        }

        public Enrollment GetEnrollmetnInfo(String username, String courseId)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(courseId))
                throw new ArgumentNullException(" username and courseId cannot be null");

            Enrollment retVal = null;
            // check if student is enrolled  
            if (IsStudentEnrolled(username, courseId))
            {
                var enrollmentInfo = GetStudentRecord(username)
                    .StudentCourseEnrollments
                    .Where(e => e.CourseId == courseId)
                    .FirstOrDefault();

                retVal = enrollmentInfo;
            }

            return retVal;
        }
    }
}