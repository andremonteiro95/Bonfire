using BonfireWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BonfireWebApp.Models.Content;

namespace BonfireWebApp.Controllers
{
    public class ContentsController : Controller
    {
        // GET: Content
        public ActionResult Index()
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Home");

            IList<Content> list;

            using (ContentDBContext db = new ContentDBContext())
            {
                list = db.GetAllBeacons();
            }

            return View(list);
        }

        public ActionResult Delete(int id)
        {
            if (!IsUserLoggedIn())
            {
                TempData["RedirectMessage"] = "Access denied. Please login.";
                return RedirectToAction("Index", "Home");
            }

            using (ContentDBContext db = new ContentDBContext())
            {
                bool success;

                if (id <= 0)
                {
                    return RedirectToAction("Index", new { result = 0, delete = 1 });
                }

                success = db.DeleteContent(id);
                return RedirectToAction("Index", new { result = success ? 1 : 0, delete = 1 });
            }
        }

        public ActionResult Edit(string id)
        {
            if (!IsUserLoggedIn())
            {
                TempData["RedirectMessage"] = "Access denied. Please login.";
                return RedirectToAction("Index", "Home");
            }

            Beacon beacon = new Beacon();

            if (!String.IsNullOrWhiteSpace(id))
            {
                using (BeaconDBContext db = new BeaconDBContext())
                {
                    beacon = db.GetBeaconById(id);
                }
            }

            return View(beacon);
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
    }
}