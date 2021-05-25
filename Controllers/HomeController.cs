using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MechanicApp.Models;
using System.Data.Entity;
namespace MechanicApp.Controllers {
    public class HomeController : Controller {
        private DatabaseContext jobsDB = new DatabaseContext();
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
        /*
         *  SHA512 hasher = new SHA512Managed();
            byte[] pass = Encoding.Unicode.GetBytes("123452");
            byte[] res = hasher.ComputeHash(pass);
            Convert.ToBase64String(res);
         */

        public ActionResult Job(int id)
        {
            /*Job job = new Models.Job();
            job.CarManufacturer = "Honda";
            job.CarModel = "Jazz";
            job.ClientName = "Jagzam Nicram";
            job.ClientPhoneNumber = "111222333";
            
            List<Defect> defects = new List<Defect>();
            defects.Add(new Defect("Silnik wyjebało"));
            defects.Add(new Defect("Skrzynie też"));
            defects.Add(new Defect("A zawieszenia to nigdy nie było"));
            job.Defects = defects;
            jobsDB.Jobs.Add(job);
            jobsDB.SaveChanges();*/
            Job tmpjob = jobsDB.Jobs.Include(j => j.Defects).SingleOrDefault(j => j.Id == id);
            return View(tmpjob);
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