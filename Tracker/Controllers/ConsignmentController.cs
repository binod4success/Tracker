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


        public ActionResult EditConsignment()
        {
            var model = new ConsignmentViewModel();
            model.ConsignmentList = _tarckingRepos.GetAllConsignmentDetails();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddConsignment(Consignment newConsignment)
        {
            _consignmentRepos.InsertConsignment(newConsignment);
            return RedirectToAction("Index");
        }
    }
}