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
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public ContentType CourseContentType { get; set; }
        public string ResourcePath { get; set; }
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
