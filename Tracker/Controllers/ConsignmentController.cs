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
    public class ConsignmentController : Controller
    {
        private static readonly ITrackingDetails _tarckingRepos = new TrackingRepository();
        private static readonly IConsignment _consignmentRepos = new ConsignmentRepository();
        //
        // GET: /Consignment/
        public ActionResult Index()
        {
            var model = new ConsignmentViewModel
            {
                NewConsignment = new Models.Consignment
                {
                    TrackingId = RepositoryHelper.GenerateRandomId()
                }
            };
            return View(model);
        }


        public ActionResult ShowConsignmentList(string mode)
        {
            var model = new ConsignmentViewModel
            {
                Mode = mode,
                ConsignmentList = _tarckingRepos.GetAllConsignmentDetails()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateConsignment(Consignment newConsignment)
        {
            _consignmentRepos.UpdateConsignment(newConsignment);
            return RedirectToAction("ShowConsignmentList", new { Mode = "Edit" });
        }

        [HttpPost]
        public ActionResult AddConsignment(Consignment newConsignment)
        {
            _consignmentRepos.InsertConsignment(newConsignment);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteConsignment(string consignmentId)
        {
            _consignmentRepos.DeleteConsignment(consignmentId);
            return RedirectToAction("ShowConsignmentList", new { Mode = "Delete"});
        }
    }
}