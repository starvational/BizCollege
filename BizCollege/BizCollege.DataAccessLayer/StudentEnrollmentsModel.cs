using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizCollege.DataAccessLayer.Repository;
using BizCollege.DataAccessLayer.Domain;

namespace BizCollege.DataAccessLayer
{
    /// <summary>
    /// Used to interface with the BizCollege student enrollments repository: to
    /// enroll a student in a course, update a student's enrollment in a course,
    /// as well as retrieve a student's current course enrollment information.
    /// </summary>
    public class StudentEnrollmentsModel : IStudentEnrollmentsModel
    {
        private IRepository<StudentRecord, string> m_repo;

        public StudentEnrollmentsModel()
        {
            m_repo = new BizCollegeRepository<StudentRecord, string>();
        }

        public void EnrollStudentInCourse(string username, string courseId)
        {
            IRepository<Course, string> coursesRepo = new BizCollegeRepository<Course, string>();
            var course = coursesRepo.Get(courseId);
            if (course == null)
                throw new InvalidOperationException("That course does not exist in the system");

            var student = m_repo.Get(username);
            if (student == null)
            {
                student = new StudentRecord() { Username = username, StudentCourseEnrollments = new HashSet<Enrollment>() };
                student.StudentCourseEnrollments.Add(new Enrollment() { CourseId = courseId, DateStarted = DateTime.Now });
            }
            else
            {
                // todo:  handle the case of enrolling in the same course more than once
                student.StudentCourseEnrollments.Add(new Enrollment() { CourseId = courseId, DateStarted = DateTime.Now });
            }
        }

        public void SetStudentCourseCompletion(string username, string courseId)
        {
            IRepository<Course, string> coursesRepo = new BizCollegeRepository<Course, string>();
            var course = coursesRepo.Get(courseId);
            if (course == null)
                throw new InvalidOperationException("That course does not exist in the system");

            var student = m_repo.Get(username);
            if (student == null)
            {
                throw new InvalidOperationException("That user does not have any enrollments in the system");
            }
            else
            {
                bool foundCourseEnrollmentToSetCompleted = false;

                foreach (var enrollment in student.StudentCourseEnrollments)
                {
                    if (enrollment.CourseId.Equals(courseId))
                    {
                        enrollment.WasCourseCompleted = true;
                        enrollment.DateCompleted = DateTime.Now;
                        foundCourseEnrollmentToSetCompleted = true;
                        break;
                    }
                }

                if (!foundCourseEnrollmentToSetCompleted)
                    throw new InvalidOperationException("That user was not enrolled in that course");
            }
        }

        public Domain.StudentRecord GetStudentEnrollmentInfo(string username)
        {
            return m_repo.Get(username);
        }
    }
}
