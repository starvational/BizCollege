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
        public IList<Enrollment> StudentCourseEnrollments { get; set; }

        public override bool Equals(object obj)
        {
            StudentRecord other = obj as StudentRecord;
            if (other == null)
            {
                return false;
            }
            else
            {
                return this.Username.Equals(other.Username);
            }
        }

        public override int GetHashCode()
        {
            return this.Username.GetHashCode();
        }
    }
}
