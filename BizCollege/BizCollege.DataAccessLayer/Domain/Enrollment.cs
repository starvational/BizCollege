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

        /// <summary>
        /// The CourseContent.Id of the the last course Slide the user
        /// viewed/accessed.  So when the user resumes a Course, the
        /// BizCollege web application will resume from the last course
        /// Slide viewed.
        /// 
        /// TODO:  If we're retrieving Slides by their assigned index, we probably don't need to
        ///        also keep track of them by their Id.  We should either track last slide accessed
        ///        either by its assigned CourseContent.IndexInSquence or CourseContent.Id, but
        ///        probably not both (for now, we'll keep them both until we can talk about this
        ///        as a team)
        /// </summary>
        public string IdOfLastCourseSlideAccessed { get; set; }

        /// <summary>
        /// The Slide index of the last viewed CourseContent slide, this slide
        /// index property should match the CourseContent.IndexInSquence property
        /// in one of the Course.CourseSlides
        /// </summary>
        public int LastViewedSlideIndex { get; set; }

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
