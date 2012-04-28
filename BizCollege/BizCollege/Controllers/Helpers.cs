using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizCollegeItr.Models;
using System.Web.Mvc;


namespace BizCollegeItr.MvcHelpers
{
    public static class Helpers
    {
        public static MvcHtmlString GetCompetionStatus(this HtmlHelper helper, int competionStatueCode)
        {
            String retVal = String.Empty;
            var status = (CourseCompletionStatus)competionStatueCode;
            switch (status)
            {
                case CourseCompletionStatus.Started:
                    retVal = "Started";
                    break;
                case CourseCompletionStatus.Completed:
                    retVal = "Completed";
                    break;
                case CourseCompletionStatus.Unkown:
                case CourseCompletionStatus.NotStarted:
                default:

                    retVal = "Not Started";

                    break;

            }

            return new MvcHtmlString(retVal);
        }
    }
}