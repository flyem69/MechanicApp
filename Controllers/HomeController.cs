using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MechanicApp.Models;
using System.Data.Entity;
namespace MechanicApp.Controllers {
    public class HomeController : Controller {
        private DatabaseContext DBContext = new DatabaseContext();
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

        public JsonResult SignIn(string login, string pass) {
            var user = new User();
            user.Name = login;
            user.Password = pass;
            try {
                DBContext.Users.Single(u => u.Name.Equals(user.Name) && u.HashedPassword.Equals(user.HashedPassword));
                return Json(new { success = 1 });
            } catch (Exception) {
                return Json(new { success = 0 });
            }
        }

        public JsonResult SignUp(string login, string pass) {
            try {
                DBContext.Users.Single(u => u.Name.Equals(login));
                return Json(new { result = "exists" });
            } catch (Exception) {
                if (
                    login.Length == 0 || login.Length > 32 ||
                    pass.Length < 8 || pass.Length > 32) {
                    return Json(new { result = "size" });
                }
                User user = new User();
                user.Name = login;
                user.Password = pass;
                DBContext.Users.Add(user);
                DBContext.SaveChanges();
                return Json(new { result = "ok" });
            }
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
            User data = DBContext.Users.Include(u => u.Jobs.Select(j => j.Defects)).SingleOrDefault(u => u.Id == 1);
            
            if(data != null)
            {
                foreach (Job job in data.Jobs)
                {
                    if (job.Id == id)
                    {
                        return View(job);
                    }
                }
            }

            //Job tmpjob = DBContext.Users.;//.Jobs.Include(j => j.Defects).SingleOrDefault(j => j.Id == id);
            return View("Index");
        }

        /*public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }*/

        public ActionResult List(int id)
        {

            User data = DBContext.Users.Where(u => u.Id == id).Include(u => u.Jobs).FirstOrDefault();

            if (data != null)
            {
              return View(data);
            }

            return View("Index");
        }
    }
}