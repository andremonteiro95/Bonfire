using BonfireWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BonfireWebApp.Controllers
{
    public class BeaconsController : Controller
    {
        // GET: Beacon
        public ActionResult Index()
        {
            if (!IsUserLoggedIn())
            {
                TempData["RedirectMessage"] = "Access denied. Please login.";
                return RedirectToAction("Index", "Home");
            }

            IList<Beacon> list;

            using (BeaconDBContext db = new BeaconDBContext())
            {
                list = db.GetAllBeacons();
            }

            return View(list);
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

        [HttpPost]
        public ActionResult Save(Beacon beacon)
        {
            if (!IsUserLoggedIn())
            {
                TempData["RedirectMessage"] = "Access denied. Please login.";
                return RedirectToAction("Index", "Home");
            }

            if (String.IsNullOrWhiteSpace(beacon.uuid))
            {
                ModelState.AddModelError("uuid", "Please fill this field.");
                return View("Edit", beacon);
            }

            try
            {
                new Guid(beacon.uuid);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("uuid", "UUID must have 32 characters, separated by 4 dashes.");
                beacon.uuid = null;
                return View("Edit", beacon);
            }

            if (String.IsNullOrWhiteSpace(beacon.Name))
            {
                ModelState.AddModelError("Name", "Please fill this field.");
                return View("Edit", beacon);
            }

            if (String.IsNullOrWhiteSpace(beacon.Localization))
            {
                ModelState.AddModelError("Localization", "Please fill this field.");
                return View("Edit", beacon);
            }

            using (BeaconDBContext db = new BeaconDBContext())
            {
                bool success;

                if (Request.Params["typeofedit"].ToString().Equals("new")) // hiddenfield can be 2 values: new, exists
                {
                    success = db.AddNewBeacon(beacon);
                    return RedirectToAction("Index", new { result = success ? 1 : 0, add = 1 });
                }

                success = db.EditBeacon(beacon);
                return RedirectToAction("Index", new { result = success ? 1 : 0, add = 0 });
            }
        }

        public ActionResult Delete(string id)
        {
            if (!IsUserLoggedIn())
            {
                TempData["RedirectMessage"] = "Access denied. Please login.";
                return RedirectToAction("Index", "Home");
            }

            using (BeaconDBContext db = new BeaconDBContext())
            {
                bool success;

                if (String.IsNullOrWhiteSpace(id))
                {
                    return RedirectToAction("Index", new { result = 0, delete = 1 });
                }

                success = db.DeleteBeacon(id);
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