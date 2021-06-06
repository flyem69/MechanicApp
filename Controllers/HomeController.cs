﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MechanicApp.Models;
using System.Data.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MechanicApp.Controllers {
    public class HomeController : Controller {
        private DatabaseContext DBContext = new DatabaseContext();
        public ActionResult Login() {
            if (Session["UserId"] != null) {
                return RedirectToAction("List", "Home");
            } else {
                return View();
            }
        }

        public ActionResult Register() {
            if (Session["UserId"] != null) {
                return RedirectToAction("List", "Home");
            } else {
                return View();
            }
        }

        [HttpPost]
        public JsonResult SignIn(string login, string pass) {
            var user = new User();
            user.Name = login;
            user.Password = pass;
            try {
                User signedInUser = DBContext.Users.Single(u => u.Name.Equals(user.Name) && u.HashedPassword.Equals(user.HashedPassword));
                Session["UserId"] = signedInUser.Id;
                return Json(new { success = 1 });
            } catch (Exception) {
                return Json(new { success = 0 });
            }
        }

        [HttpPost]
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
                Session["UserId"] = user.Id;
                return Json(new { result = "ok" });
            }
        }
        /*
         *  SHA512 hasher = new SHA512Managed();
            byte[] pass = Encoding.Unicode.GetBytes("123452");
            byte[] res = hasher.ComputeHash(pass);
            Convert.ToBase64String(res);
         */

        public ActionResult Job(int id) {
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
            if (Session["UserId"] == null) {
                return RedirectToAction("Login", "Home");
            } else {
                int userID = Convert.ToInt32(Session["UserID"]);
                User data = DBContext.Users.Include(u => u.Jobs.Select(j => j.Defects)).SingleOrDefault(u => u.Id == userID);

                if (data != null) {
                    foreach (Job job in data.Jobs) {
                        if (job.Id == id) {
                            return View(job);
                        }
                    }
                }
            }


            //Job tmpjob = DBContext.Users.;//.Jobs.Include(j => j.Defects).SingleOrDefault(j => j.Id == id);
            return View("Index");
        }

        public ActionResult List() {
            if (Session["UserId"] == null) {
                return RedirectToAction("Login", "Home");
            } else {
                int id = Convert.ToInt32(Session["UserId"]);
                User data = DBContext.Users.Where(u => u.Id == id).Include(u => u.Jobs).FirstOrDefault();

                if (data != null) {
                    return View(data);
                }

                Session.Abandon();
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult ManageJob(Job job) {
            return View(job);
        }

        [HttpPost]
        public ActionResult AddJob(string jobString) {
            try {
                JObject jobJSON = JObject.Parse(jobString);
                Job job = new Job();
                job.CarManufacturer = (string)jobJSON["CarManufacturer"];
                job.CarModel = (string)jobJSON["CarModel"];
                job.ClientName = (string)jobJSON["ClientName"];
                job.ClientPhoneNumber = (string)jobJSON["ClientPhoneNumber"];
                JArray defectsJSON = (JArray)jobJSON["Defects"];
                List<Defect> defects = defectsJSON.Select(d => new Defect((string)d)).ToList();
                job.Defects = defects;
                User user = DBContext.Users.Include(u => u.Jobs.Select(j => j.Defects)).Single(u => u.Id == 1);
                user.Jobs.Add(job);
                DBContext.SaveChanges();
                return Json(new { success = 1 });
            } catch(Exception) {
                return Json(new { success = 0 });
            }
        }

        [HttpPost]
        public ActionResult EditJob(Job model) {
            if (model == null) {
                return Json(new { success = 0 });
            } else {
                try {
                    int id = Convert.ToInt32(Session["UserId"]);
                    User user = DBContext.Users.Include(u => u.Jobs.Select(j => j.Defects)).Single(u => u.Id == 1);
                    bool editSuccess = false;
                    for (int i = 0; i < user.Jobs.Count; i++) {
                        if (user.Jobs[i].Id == model.Id) {
                            /*user.Jobs[i].CarManufacturer = job.CarManufacturer;
                            user.Jobs[i].CarModel = job.CarModel;
                            user.Jobs[i].ClientName = job.ClientName;
                            user.Jobs[i].ClientPhoneNumber = job.ClientPhoneNumber;
                            user.Jobs[i].*/
                            user.Jobs[i] = model;
                            DBContext.SaveChanges();
                            editSuccess = true;
                        }
                    }
                    if (editSuccess)
                        return Json(new { success = 1 });
                    else
                        return Json(new { success = 0 });
                } catch (Exception) {
                    return Json(new { success = 0 });
                }
            }
        }
    }
}