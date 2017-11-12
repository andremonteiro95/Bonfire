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
            // TODO
            if (!IsUserLoggedIn() /*|| !IsUserAdmin()*/)
            {
                TempData["RedirectMessage"] = "Access denied. Please login.";
                return RedirectToAction("Index", "Home");
            }

            IList<User> list;

            using(UserDBContext db = new UserDBContext())
            {
                list = db.GetAllUsers();
            }

            return View(list);
        }

        private bool IsUserLoggedIn()
        {
            if (Session["UserID"] == null)
                return false;
            
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