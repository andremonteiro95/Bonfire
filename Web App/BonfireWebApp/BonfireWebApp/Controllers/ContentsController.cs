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

        private bool IsUserLoggedIn()
        {
            if (Session["UserID"] == null)
                return false;

            return true;
        }
    }
}