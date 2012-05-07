using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BizCollege.DataAccessLayer.Domain;
using BizCollege.DataAccessLayer.Repository;
using BizCollege.DataAccessLayer.Tests.Helper;

namespace BizCollege.DataAccessLayer.Tests
{
    [TestFixture]
    public class StudentEnrollments_Fixture
    {
        [SetUp]
        public void Setup()
        {
            // Uncomment to debug the tests when using NUnit gui
            System.Diagnostics.Debugger.Launch();
        }

        [Test]
        public void CanAddNewStudentEnrollment()
        {
            // dummy username (provided by an external accounts
            // data store (e.g. ASP .net membership/authentication)
            string username = "kevinmitnick";

            // Create a dummy course and add it to the database (since the student
            // enrollments model will not allow an enrollment to be added to a student
            // for a course that doesn't exist in the database)
            var dummyCourse = DummyDataGenerator.CreateDummyCourse();
            ICoursesModel courseModel = new CoursesModel();
            dummyCourse.Id = courseModel.AddOrUpdateCourse(dummyCourse).Id;


            // add the new enrollment for the given user and specified course
            IStudentEnrollmentsModel model = new StudentEnrollmentsModel();
            StudentRecord studentEnrollmentRecord = model.AddEnrollment(username, dummyCourse.Id);

            Assert.NotNull(studentEnrollmentRecord);
            Assert.IsNotNullOrEmpty(studentEnrollmentRecord.Username);
            Assert.NotNull(studentEnrollmentRecord.StudentCourseEnrollments);
            Assert.AreEqual(studentEnrollmentRecord.StudentCourseEnrollments.Count, 1);

            Assert.AreEqual(studentEnrollmentRecord.Username, username);

            var enrollments = new List<Enrollment>(studentEnrollmentRecord.StudentCourseEnrollments);
            Assert.AreEqual(enrollments[0].CourseId, dummyCourse.Id);

            // clean up Db:  remove student record
            var enrollmentsRepo = new BizCollegeRepository<StudentRecord, string>();
            enrollmentsRepo.Remove(studentEnrollmentRecord.Username);

            // clean up Db:  remove dummy course
            var coursesRepo = new BizCollegeRepository<Course, string>();
            coursesRepo.Remove(dummyCourse.Id);
        }

        [Test]
        public void CanRemoveStudentEnrollment()
        {
            // Create a dummy student enrollment record
            string username = "kevinmitnick";

            var dummyCourse = DummyDataGenerator.CreateDummyCourse();
            ICoursesModel courseModel = new CoursesModel();
            dummyCourse.Id = courseModel.AddOrUpdateCourse(dummyCourse).Id;


            // add the new enrollment for the given user and specified course
            IStudentEnrollmentsModel model = new StudentEnrollmentsModel();
            StudentRecord studentEnrollmentRecord = model.AddEnrollment(username, dummyCourse.Id);
        }

        [Test]
        public void CanUpdateExistingStudentEnrollment()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CanGetStudentEnrollment()
        {
            throw new NotImplementedException();
        }
    }
}
