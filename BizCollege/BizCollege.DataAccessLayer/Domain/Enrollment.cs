using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizCollege.DataAccessLayer.Domain
{
    /// <summary>
    /// An enrollment is used to track the status of a single
    /// course enrollment by a student.  When a student enrolls
    /// in a course, an enrollment is created and added to the
    /// student's record.  When the student completes the course,
    /// the enrollment for that course is updated and marked
    /// as completed.
    /// </summary>
    public class Enrollment
    {
        /// <summary>
        /// The internal Id of the item in the database (assigned
        /// by NHibernate after the item is initially persisted)
        /// </summary>
        public string Id { get; set; }
        public string CourseId { get; set; }
        public DateTime DateStarted { get; set; }

        public bool WasCourseCompleted { get; set; }
        public DateTime DateCompleted { get; set; }
        public string IdOfLastCourseSlideAccessed { get; set; }

        public override bool Equals(object obj)
        {
            Enrollment other = obj as Enrollment;
            if (other == null)
            {
                return false;
            }
            else
            {
                return this.Id.Equals(other.Id);
            }
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
