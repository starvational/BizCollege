using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BizCollege.DataAccessLayer.Domain;
using BizCollege.DataAccessLayer.Repository;
using BizCollege.DataAccessLayer.Tests.Helper;
using BizCollege.DataAccessLayer.Helper;

namespace BizCollege.DataAccessLayer.Tests
{
    [TestFixture]
    public class StudentEnrollmentsFixture
    {
        [SetUp]
        public void Setup()
        {
            // Uncomment to debug the tests when using NUnit gui
            //System.Diagnostics.Debugger.Launch();
        }

        [Test]
        public void CanAddNewStudentCourseEnrollment()
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
            Assert.AreEqual(studentEnrollmentRecord.Username, username);
            Assert.NotNull(studentEnrollmentRecord.StudentCourseEnrollments);
            Assert.AreEqual(studentEnrollmentRecord.StudentCourseEnrollments.Count, 1);

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
            // dummy username and course id
            string username = "kevinmitnick";
            string courseId = Guid.NewGuid().ToString();

            // Add the dummy student enrollment via the internal repository interfaces
            var dummyEnrollment = new StudentRecord() { Username = username };
            dummyEnrollment.StudentCourseEnrollments = new List<Enrollment>();
            dummyEnrollment.StudentCourseEnrollments.Add(
                new Enrollment()
                {
                    CourseId = courseId,
                    DateStarted = DateTime.Now,
                    DateCompleted = SqlServerHelper.GetSqlServerMinimumDateTimeValue()
                }
            );
            var enrollmentsRepo = new BizCollegeRepository<StudentRecord, string>();
            dummyEnrollment = enrollmentsRepo.AddOrUpdate(dummyEnrollment);

            // Remove the enrollment via the enrollmnent model interface that the webapp
            // will use to remove a student's enrollment from their record
            IStudentEnrollmentsModel enrollmentsModel = new StudentEnrollmentsModel();
            enrollmentsModel.RemoveEnrollment(username, courseId);

            // retrieve the enrollment record from the Db and ensure the student has
            // no enrollments in his/her student record
            var fromDb = enrollmentsRepo.Get(username);

            Assert.NotNull(fromDb);
            Assert.AreEqual(fromDb.Username, username);
            Assert.AreEqual(fromDb.StudentCourseEnrollments.Count, 0);

            // clean up Db
            enrollmentsRepo.Remove(fromDb.Username);
        }

        [Test]
        public void CanUpdateExistingStudentEnrollmentsCollection()
        {
            // dummy username and dummy courses
            string username = "kevinmitnick";
            var dummyCourse1 = DummyDataGenerator.CreateDummyCourse();
            var dummyCourse2 = DummyDataGenerator.CreateDummyCourse();

            // Add the dummy courses to the database via the internal course repository interface
            IRepository<Course, string> coursesRepo = new BizCollegeRepository<Course, string>();
            dummyCourse1.Id = coursesRepo.AddOrUpdate(dummyCourse1).Id;
            dummyCourse2.Id = coursesRepo.AddOrUpdate(dummyCourse2).Id;

            // Add the dummy student enrollment via the internal enrollments repository interfaces
            var dummyEnrollment = new StudentRecord() { Username = username };
            dummyEnrollment.StudentCourseEnrollments = new List<Enrollment>();
            dummyEnrollment.StudentCourseEnrollments.Add(
                new Enrollment()
                {
                    CourseId = dummyCourse1.Id,
                    DateStarted = DateTime.Now,
                    DateCompleted = SqlServerHelper.GetSqlServerMinimumDateTimeValue()
                }
            );

            var enrollmentsRepo = new BizCollegeRepository<StudentRecord, string>();
            dummyEnrollment = enrollmentsRepo.AddOrUpdate(dummyEnrollment);

            // To update the student enrollment, we'll add another enrollment to their
            // current enrollment record (so we should have two enrollments after the update)
            IStudentEnrollmentsModel model = new StudentEnrollmentsModel();
            var updatedRecord = model.AddEnrollment(username, dummyCourse2.Id);

            Assert.NotNull(updatedRecord);
            Assert.IsNotNullOrEmpty(updatedRecord.Username);
            Assert.AreEqual(updatedRecord.Username, username);

            Assert.NotNull(updatedRecord);
            Assert.IsNotNullOrEmpty(updatedRecord.Username);
            Assert.NotNull(updatedRecord.StudentCourseEnrollments);
            Assert.AreEqual(updatedRecord.StudentCourseEnrollments.Count, 2);

            var updatedEnrollments = new List<Enrollment>(updatedRecord.StudentCourseEnrollments);
            Assert.AreEqual(updatedEnrollments[0].CourseId, dummyCourse1.Id);
            Assert.AreEqual(updatedEnrollments[1].CourseId, dummyCourse2.Id);

            // Clean up Db
            enrollmentsRepo.Remove(username);
            coursesRepo.Remove(dummyCourse1.Id);
            coursesRepo.Remove(dummyCourse2.Id);
        }

        [Test]
        public void CanGetStudentEnrollment()
        {
            // dummy username and course id
            string username = "kevinmitnick";
            string courseId = Guid.NewGuid().ToString();

            // Add the dummy student enrollment via the internal repository interfaces
            var dummyEnrollment = new StudentRecord(){ Username = username};
            dummyEnrollment.StudentCourseEnrollments = new List<Enrollment>();
            dummyEnrollment.StudentCourseEnrollments.Add(
                new Enrollment()
                {
                    CourseId = courseId,
                    DateStarted = DateTime.Now,
                    DateCompleted = SqlServerHelper.GetSqlServerMinimumDateTimeValue()
                }
            );
            var enrollmentsRepo = new BizCollegeRepository<StudentRecord, string>();
            dummyEnrollment = enrollmentsRepo.AddOrUpdate(dummyEnrollment);

            // Get the student record via the enrollments model interface that the web
            // application will use to retrieve student enrollment records by username
            IStudentEnrollmentsModel enrollmentsModel = new StudentEnrollmentsModel();
            var fromDb = enrollmentsModel.GetStudentRecord(username);

            Assert.NotNull(fromDb);
            Assert.AreEqual(fromDb.Username, dummyEnrollment.Username);
            Assert.NotNull(fromDb.StudentCourseEnrollments);
            Assert.AreEqual(fromDb.StudentCourseEnrollments.Count, dummyEnrollment.StudentCourseEnrollments.Count);
            Assert.AreEqual(fromDb.StudentCourseEnrollments[0].Id, dummyEnrollment.StudentCourseEnrollments[0].Id);
            Assert.AreEqual(fromDb.StudentCourseEnrollments[0].CourseId, dummyEnrollment.StudentCourseEnrollments[0].CourseId);
            Assert.AreEqual(fromDb.StudentCourseEnrollments[0].DateCompleted, dummyEnrollment.StudentCourseEnrollments[0].DateCompleted);

            // Clean up Db
            enrollmentsRepo.Remove(username);
        }

        [Test]
        public void CanSetStudentEnrollmentLastViewedSlide()
        {
            string username = "kevinmitnick";

            // Create a dummy course
            var dummyCourse = DummyDataGenerator.CreateDummyCourse();
            ICoursesModel courseModel = new CoursesModel();
            dummyCourse.Id = courseModel.AddOrUpdateCourse(dummyCourse).Id;

            // add a new enrollment for the given user and specified dummy course
            IStudentEnrollmentsModel model = new StudentEnrollmentsModel();
            StudentRecord studentEnrollmentRecord = model.AddEnrollment(username, dummyCourse.Id);

            // Simulate that the user last viewed/accessed slide 5 of the dummy course
            int lastViewedSlideIndex = 5;

            // Update the student enrollment for the dummy course with the specified last viewed slide index
            StudentRecord updatedRecord = model.SetStudentEnrollmentLastViewedSlide(username, dummyCourse.Id, lastViewedSlideIndex);

            Assert.NotNull(updatedRecord);
            Assert.AreEqual(updatedRecord.StudentCourseEnrollments[0].LastViewedSlideIndex, lastViewedSlideIndex);

            // clean up Db:  remove student record
            var enrollmentsRepo = new BizCollegeRepository<StudentRecord, string>();
            enrollmentsRepo.Remove(studentEnrollmentRecord.Username);

            // clean up Db:  remove dummy course
            var coursesRepo = new BizCollegeRepository<Course, string>();
            coursesRepo.Remove(dummyCourse.Id);
        }

        [Test]
        public void CanSetStudentEnrollmentCourseCompletion()
        {
            // To be completed by someone else on the team
            // Either Ayman or Thaison
            //
            // Tip:  Use the IStudentEnrollmentsModel.AddEnrollment to add a new dummy
            //       enrollment, then IStudentEnrollmentsModel.SetStudentCourseCompletion
            //       to set the course completion for that student's course.  Then use
            //       the internal IRepository<StudentRecord, string> interface to retrive
            //       the record from the database and make sure that course was set to
            //       complete.
            //       After you're done with the test, remove the dummy enrollment from 
            //       the unit test database
            throw new NotImplementedException();
        }
    }
}
