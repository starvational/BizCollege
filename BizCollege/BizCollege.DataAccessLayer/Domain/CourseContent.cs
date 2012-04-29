using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizCollege.DataAccessLayer.Domain
{
    /// <summary>
    /// A CourseContent is a single unit of content (e.g. video, audio, etc...)
    /// which is part of a Course.  A single instance represents a slide that is
    /// shown to the user as part of a Course.
    /// </summary>
    public class CourseContent
    {
        /// <summary>
        /// The internal Id of the item in the database (assigned
        /// by NHibernate after the item is initially persisted)
        /// </summary>
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public ContentType CourseContentType { get; set; }
        public string ResourcePath { get; set; }

        public override bool Equals(object obj)
        {
            CourseContent other = obj as CourseContent;
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

    public enum ContentType
    {
        Unknown,
        Audio,
        Video,
        Document,
        Presentation,
        Interactive
    }
}
