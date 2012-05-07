using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizCollege.DataAccessLayer.Domain;

namespace BizCollege.DataAccessLayer.Tests.Helper
{
    /// <summary>
    /// Helper methods that generate unpersited (not stored in the Db)
    /// domain objects with dummy data in them.  These helper methods
    /// help remove repetitive dummyd data generation code from the unit tests
    /// </summary>
    public static class DummyDataGenerator
    {
        public static Course CreateDummyCourse()
        {
            var dummyCourse = new Course();
            dummyCourse.CreatedByUsername = "miguel";
            dummyCourse.Name = "Business 101";
            dummyCourse.Description = "Learn the basics about running a business";
            dummyCourse.State = CourseState.Inactive;
            dummyCourse.LastUpdateByUsername = "miguel";
            dummyCourse.CourseSlides = new List<CourseContent>();

            return dummyCourse;
        }
    }
}
