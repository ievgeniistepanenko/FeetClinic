using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeetClinic.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Hej";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "If you have any questions please contact os:";

            return View();
        }
    }
}