using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizCollegeItr.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}