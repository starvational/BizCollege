using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizCollege.DataAccessLayer.Repository;
using BizCollege.DataAccessLayer.Domain;
using BizCollege.DataAccessLayer.Helper;

namespace BizCollege.DataAccessLayer
{
    /// <summary>
    /// Used to interface with the BizCollege student enrollments repository: to
    /// enroll a student in a course, update a student's enrollment in a course,
    /// as well as retrieve a student's current course enrollment information.
    /// </summary>
    public class StudentEnrollmentsModel : IStudentEnrollmentsModel
    {
        private IRepository<StudentRecord, string> m_enrollmentsRepo;

        public StudentEnrollmentsModel()
        {
            m_enrollmentsRepo = new BizCollegeRepository<StudentRecord, string>();
        }

        /// <summary>
        /// Given a Course.Id and a student's username, enroll the student in that course.  If
        /// a student record doesn't exist (e.g. the student was never enrolled in a course),
        /// a record is created internally and the course is added to the StudentRecord.StudentCourseEnrollments.
        /// If the student already has a record, we udpate the student's enrollments with the new course
        /// enrollment.
        /// </summary>
        /// <param name="username">The username is how we uniquely track/identify a StudentRecord</param>
        /// <param name="courseId">The Course.Id (unique id of the course)</param>
        /// <exception cref="System.InvalidOperationException">
        ///     <para>
        ///     1) If the given course (by Course.Id) does not exist in the system
        ///     2) If the student is already enrolled in that course
        ///     </para>
        /// </exception>
        /// <returns>A copy of the persisted/updated StudentRecord</returns>
        public StudentRecord AddEnrollment(string username, string courseId)
        {
            IRepository<Course, string> coursesRepo = new BizCollegeRepository<Course, string>();
            var course = coursesRepo.Get(courseId);
            if (course == null)
                throw new InvalidOperationException("That course does not exist in the system");

            // Try and retrive the student's record (by username).  If they don't
            // have an existing record, create a record for them
            var student = m_enrollmentsRepo.Get(username);
            if (student == null)
            {
                student = new StudentRecord() { Username = username, StudentCourseEnrollments = new List<Enrollment>() };
            }

            // Throw an error if the student is already enrolled in this course
            foreach (var currentEnrollment in student.StudentCourseEnrollments)
            {
                if (currentEnrollment.CourseId == courseId)
                {
                    throw new InvalidOperationException("The student is already enrolled in course:  " + courseId);
                }
            }

            // Add the new enrollment and save it the updated student record
            student.StudentCourseEnrollments.Add(new Enrollment()
            {
                CourseId = courseId,
                DateStarted = DateTime.Now,
                DateCompleted = SqlServerHelper.GetSqlServerMinimumDateTimeValue()
            });

            return m_enrollmentsRepo.AddOrUpdate(student);
        }

        /// <summary>
        /// Removes the specified course from the student's enrollment record
        /// </summary>
        /// <param name="username">The student's username</param>
        /// <param name="courseId">The unique id of the course (Course.Id)</param>
        /// <exception cref="System.InvalidOperationException">
        ///     <para>
        ///     1) If the student does not have a student record (aka no enrollments in the system)
        ///     2) If the student was never enrolled in that course to begin with
        ///     </para>
        /// </exception>
        public void RemoveEnrollment(string username, string courseId)
        {
            var student = m_enrollmentsRepo.Get(username);
            if (student == null)
            {
                throw new InvalidOperationException("That user does not have a student enrollment record the system");
            }
            else
            {
                Enrollment enrollmentToRemove = null;
                foreach (var enrollment in student.StudentCourseEnrollments)
                {
                    if (enrollment.CourseId.Equals(courseId))
                    {
                        enrollmentToRemove = enrollment;
                        break;
                    }
                }

                if (enrollmentToRemove == null)
                {
                    throw new InvalidOperationException("That user was not enrolled in that course: " + courseId);
                }
                else
                {
                    student.StudentCourseEnrollments.Remove(enrollmentToRemove);
                    m_enrollmentsRepo.AddOrUpdate(student);
                }
            }
        }

        /// <summary>
        /// Retrives the student's record from the database
        /// </summary>
        /// <param name="username">The username of the student is the unique Id of the StudentRecord</param>
        /// <returns>Will return the persisted Record if it exists, Null if there is no record for that username</returns>
        public StudentRecord GetStudentRecord(string username)
        {
            return m_enrollmentsRepo.Get(username);
        }

        /// <summary>
        /// Mark's that student's record with that course as being completed
        /// </summary>
        /// <param name="username">The student's username</param>
        /// <param name="courseId">The unique id of the course (Course.Id)</param>
        /// <exception cref="System.InvalidOperationException">
        ///     <para>
        ///     1) If the student does not have a student record (aka no enrollments in the system)
        ///     2) If the student was never enrolled in that course to begin with
        ///     </para>
        /// </exception>
        public void SetStudentCourseCompletion(string username, string courseId)
        {
            var student = m_enrollmentsRepo.Get(username);
            if (student == null)
            {
                throw new InvalidOperationException("That user does not have a student enrollment record the system");
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
                {
                    throw new InvalidOperationException("That user was not enrolled in that course: " + courseId);
                }
                else
                {
                    m_enrollmentsRepo.AddOrUpdate(student);
                }
            }
        }
    }
}
