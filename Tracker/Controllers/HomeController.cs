using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracker.Models;
using Tracker.Repository;
using Tracker.ViewModels;

namespace Tracker.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ITrackingDetails _repos = new TrackingRepository();

        public ActionResult Index()
        {
            ViewBag.Message = "List of the currently working postmen in field.";
            var model = new IndexViewModel
            {
                ConsignmentList = _repos.GetAllConsignmentDetails()
            };
            return View(model);
        }

        public ActionResult TrackConsignment(string TrackingId)
        {
            var model = new TrackConsignmentViewModel
            {
                TrackingId = TrackingId
            };
            if (string.IsNullOrWhiteSpace(TrackingId))
            {
                model.TrackingLocations = new List<TrackingDetails>();
            }
            else
            {
                model.TrackingLocations = _repos.GetTrackingDetails(TrackingId);
            }



            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
