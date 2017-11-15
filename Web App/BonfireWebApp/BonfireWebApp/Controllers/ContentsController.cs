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

        private bool IsUserLoggedIn()
        {
            if (Session["UserID"] == null)
                return false;

            return true;
        }
    }
}