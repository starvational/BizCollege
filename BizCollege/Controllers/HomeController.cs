using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizCollegeMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "bizCollege";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
