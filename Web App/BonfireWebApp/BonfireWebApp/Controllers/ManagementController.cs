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
            if (Session["UserID"] == null)
            {
                TempData["RedirectMessage"] = "Access denied. Please login.";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}