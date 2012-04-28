using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BizCollegeItr.Models
{
    public enum CourseCompletionStatus
    {
        Unkown,
        NotStarted,
        Started,
        Completed
    }
    public class CourseHistory
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CourseStatus { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime CompletedOn { get; set; } 

        // one-to-many relation. 
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}