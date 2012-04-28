using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizCollege.DataAccessLayer.Domain
{
    /// <summary>
    /// A student record is used to keep track of a student's
    /// course enrollments.  Students are uniquely identified by
    /// their user name in the system (managed by ASP .Net membership)
    /// </summary>
    public class StudentRecord
    {
        public string Username { get; set; }
        public HashSet<Enrollment> StudentCourseEnrollments { get; set; }
    }
}
