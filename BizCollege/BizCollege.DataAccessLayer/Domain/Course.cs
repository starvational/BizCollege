using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizCollege.DataAccessLayer.Domain
{
    /// <summary>
    /// Each unique Bizcollege course is made up of a sequence of Course slides; where
    /// each slide in a course represents some content (e.g. video, audio, document)
    /// that is presented to the user.
    /// 
    /// The course could the called "Business 101" and could be made up of
    /// some sequential list of course slides as follows:
    /// 
    ///     -Slide 1:  Introduction
    ///     -Slide 2:  Business practices
    ///     -Slide 3:  Business Law
    ///     
    /// The slides are presented to the user in the sequence they are stored in the list.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// The internal Id of the item in the database (assigned
        /// by NHibernate after the item is initially persisted)
        /// </summary>
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string CreatedByUsername { get; set; }
        public DateTime CreationDate { get; set; }

        public DateTime LastUpdated { get; set; }
        public string LastUpdateByUsername { get; set; }

        public CourseState State { get; set; }
        public IList<CourseContent> CourseSlides { get; set; }

        public int NumberOfCourseCompletions { get; set; }

        public override bool Equals(object obj)
        {
            Course other = obj as Course;
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

    public enum CourseState
    {
        Inactive,
        Active,
        UnderReview
    }
}
