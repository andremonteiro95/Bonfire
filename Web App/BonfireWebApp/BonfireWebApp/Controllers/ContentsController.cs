using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BonfireWebApp.Controllers
{
    public class ContentsController : Controller
    {
        // GET: Content
        public ActionResult Index()
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "Home");

            return View();
        }

        private bool IsUserLoggedIn()
        {
            if (Session["UserID"] == null)
                return false;

            return true;
        }
    }
}