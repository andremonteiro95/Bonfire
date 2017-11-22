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
                list = db.GetAllContents();
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

        public ActionResult Edit(int id)
        {
            if (!IsUserLoggedIn())
            {
                TempData["RedirectMessage"] = "Access denied. Please login.";
                return RedirectToAction("Index", "Home");
            }

            Content content = new Models.Content();

            if (id > 0)
            {
                using (ContentDBContext db = new ContentDBContext())
                {
                    content = db.GetContentById(id);
                }
            }

            return View(content);
        }

        [HttpPost]
        public ActionResult Save(Content content)
        {
            if (!IsUserLoggedIn())
            {
                TempData["RedirectMessage"] = "Access denied. Please login.";
                return RedirectToAction("Index", "Home");
            }

            if (String.IsNullOrWhiteSpace(content.Title))
            {
                ModelState.AddModelError("Title", "Please fill this field.");
                return View("Edit", content);
            }

            if (String.IsNullOrWhiteSpace(content.Description))
            {
                ModelState.AddModelError("Description", "Please fill this field.");
                return View("Edit", content);
            }

            if (String.IsNullOrWhiteSpace(content.StartDate))
            {
                ModelState.AddModelError("StartDate", "Please fill this field.");
                return View("Edit", content);
            }

            if (String.IsNullOrWhiteSpace(content.EndDate))
            {
                ModelState.AddModelError("EndDate", "Please fill this field.");
                return View("Edit", content);
            }

            DateTime sd = DateTime.Parse(content.StartDate), ed = DateTime.Parse(content.EndDate);

            if (sd > ed)
            {
                ModelState.AddModelError("EndDate", "End Date must be after Start Date.");
                return View("Edit", content);
            }

            using (ContentDBContext db = new ContentDBContext())
            {
                bool success;

                if (content.id <= 0)
                {
                    success = db.AddContent(content);
                    return RedirectToAction("Index", new { result = success ? 1 : 0, add = 1 });
                }
                
                success = db.EditContent(content);
                return RedirectToAction("Index", new { result = success ? 1 : 0, add = 0 });
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
    }
}