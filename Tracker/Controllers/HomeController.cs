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

        private static readonly IJob _jobRepos = new JobRepository();

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

        public ActionResult StartTracker()
        {
            return View();
        }

        public ActionResult TrackMe(string jobId)
        {
            if (string.IsNullOrWhiteSpace(jobId))
            {
                ModelState.AddModelError("jobId", "JobId was missing.");
                return RedirectToAction("Tracker");
            }
            var job = _jobRepos.GetJob(jobId);
            var model = new TrackMeViewModel
            {
                JobId = Int32.Parse(jobId),
                Consignment = job.Consignment,
                Me = job.PostMan
            };
            return View(model);
        }

        public ActionResult Tracker()
        {
            var model = new TrackerViewModel
            {
                TaskList = _jobRepos.GetJobList()
            };

            return View(model);
        }

        public JsonResult GetCurrentLocation(string trackingId)
        {
            var location = _repos.GetCurrentLocation(trackingId);
            return Json(location, JsonRequestBehavior.AllowGet);
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
