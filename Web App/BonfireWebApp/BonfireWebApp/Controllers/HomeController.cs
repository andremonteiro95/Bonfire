using BonfireWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Core.Objects;

namespace BonfireWebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("Index", "Management");
            }

            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            if (TempData["RedirectMessage"] != null)
            {
                ModelState.AddModelError("Email", TempData["RedirectMessage"].ToString());
                TempData["RedirectMessage"] = null;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (String.IsNullOrWhiteSpace(user.Email) || String.IsNullOrWhiteSpace(user.Password))
            {
                return View(user);
            }

            if (!Helpers.Validations.IsValidEmail(user.Email))
            {
                ModelState.AddModelError("Email", "Email has an invalid format.");

                return View(user);
            }

            using (UserDBContext db = new UserDBContext())
            {
                User u = db.UserLogin(user.Email, user.Password.ToString());
                if (u != null)
                {
                    Session["UserID"] = u.ToString();
                    Session["UserName"] = u.ToString();
                    return RedirectToAction("Index", "Management");
                }
            }

            ModelState.AddModelError("Email", "Email and/or password invalid.");

            return View(user);
        }
    }
}