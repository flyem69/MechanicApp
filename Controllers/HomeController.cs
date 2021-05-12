using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MechanicApp.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            if (Session["LoggedIn"] != null) {
                return View("~/Views/Home/Login.cshtml");
            } else {
                return View("~/Views/Home/Login.cshtml");
            }
        }

        public ActionResult Register() {
            return View();
        }

        /*public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }*/
    }
}