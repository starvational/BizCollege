using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizCollege.DataAccessLayer.Domain;

namespace BizCollegeMvc.Models
{
    public class StudentEnrollmentViewModel
    {
        public Course Course { get; set; }
        public Enrollment EnrollmentInfo { get; set; }
    }
}