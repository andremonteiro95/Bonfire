﻿using BonfireWebApp.Helpers;
using BonfireWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BonfireWebApp.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Management
        public ActionResult Index()
        {
            if (!IsUserLoggedIn())
            {
                TempData["RedirectMessage"] = "Access denied. Please login.";
                return RedirectToAction("Index", "Home");
            }

            if (!IsUserAdmin())
                return RedirectToAction("Index", "Contents");

            IList<User> list;

            using(UserDBContext db = new UserDBContext())
            {
                list = db.GetAllUsers();
            }

            return View(list);
        }
        
        public ActionResult Edit(int id)
        {
            if (!IsUserLoggedIn())
            {
                TempData["RedirectMessage"] = "Access denied. Please login.";
                return RedirectToAction("Index", "Home");
            }

            if (!IsUserAdmin())
                return RedirectToAction("Index", "Contents");

            User user = new User();

            if (id < 0)
                return RedirectToAction("Error", "Home", new { id = 0 });

            if (id > 0)
            {
                using (UserDBContext db = new UserDBContext())
                {
                    user = db.GetUserById(id);

                    if (user.id == 0)
                        return RedirectToAction("Error", "Home", new { id = 0 });

                    return View(user);
                }
            }
            
            return View(user);
        }

        [HttpPost]
        public ActionResult Save(User user)
        {
            if (!IsUserLoggedIn() || !IsUserAdmin())
            {
                TempData["RedirectMessage"] = "Access denied. Please login.";
                return RedirectToAction("Index", "Home");
            }

            if (user.Password != null)
            if (user.Password.Length < 6)
            {
                ModelState.AddModelError("Password", "Password must be, at least, 6 characters long.");
                return View(user);
            }

            if (!Validations.IsValidEmail(user.Email))
            {
                ModelState.AddModelError("Email", "Email has an invalid format.");
                return View(user);
            }

            using (UserDBContext db = new UserDBContext())
            {
                bool success;
                
                if (user.id <= 0) {
                    success = db.AddNewUser(user);
                    return RedirectToAction("Index", new { result = success ? 1 : 0 , add = 1 });
                }

                success = db.EditUser(user);
                if (success && user.id == Int32.Parse(Session["UserID"].ToString()))
                {
                    Session["UserName"] = user.Name;
                    Session["UserPrivilege"] = user.Privilege ? "1" : "0";
                }
                return RedirectToAction("Index", new { result = success ? 1 : 0, add = 0 });
            }
        }

        public ActionResult Delete(int id)
        {
            if (!IsUserLoggedIn() || !IsUserAdmin())
            {
                TempData["RedirectMessage"] = "Access denied. Please login.";
                return RedirectToAction("Index", "Home");
            }

            if (Int32.Parse(Session["UserID"].ToString()) == id)
            {
                return RedirectToAction("Index", new { result = 0, delete = 1 });
            }

            using (UserDBContext db = new UserDBContext())
            {
                bool success;

                if (id <= 0)
                {
                    return RedirectToAction("Index", new { result = 0, delete = 1 });
                }

                success = db.DeleteUser(id);
                return RedirectToAction("Index", new { result = success ? 1 : 0, delete = 1 });
            }
        }

        private bool IsUserLoggedIn()
        {
            if (Session["UserID"] == null)
                return false;

            using (UserDBContext db = new UserDBContext())
            {
                User user = db.GetUserById(Int32.Parse(Session["UserID"].ToString()));

                if (user.id == 0) // User deleted from DB
                {
                    Session["UserID"] = null;
                    Session["UserName"] = null;
                    Session["UserPrivilege"] = null;
                    return false;
                }

                Session["UserID"] = user.id.ToString();
                Session["UserName"] = user.Name.ToString();
                Session["UserPrivilege"] = user.Privilege ? "1" : "0";
            }

            return true;
        }

        private bool IsUserAdmin()
        {
            if (Session["UserPrivilege"] != null)
            {
                if (Session["UserPrivilege"].ToString().Equals("1"))
                    return true;
            }
            return false;
        }
    }
}