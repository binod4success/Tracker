using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Tracker.Models;
using Tracker.Repository;
using Tracker.ViewModels;

namespace Tracker.Controllers
{
    public class JobController : Controller
    {
        private static readonly ITrackingDetails _repos = new TrackingRepository();

        private static readonly IPostMan _reposPostMan = new PostManRepository();

        private static readonly IJob _jobRepos = new JobRepository();

        // GET: /Job/
        public ActionResult Index()
        {
            var model = new JobViewModel
            {
                TaskList = _jobRepos.GetJobList(),
                PostMen = (from item in _reposPostMan.GetPostManList()
                           select new SelectListItem
                           {
                               Text = item.Name,
                               Value = item.PostManId.ToString()
                           }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditJob(JobViewModel model)
        {
            var data = new Job
            {
                JobId = model.JobId,
                StatusId = model.JobStatusId,
                PostMan = new PostMan
                {
                    PostManId = model.PostManId
                }
            };
            _jobRepos.UpdateJob(data);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteJob(string jobId)
        {
            //Delete Item
            if (_jobRepos.DeleteJob(jobId))
            {
                Response.StatusCode = 200;
                return Content("Job item removed");
            }
            else
            {
                Response.StatusCode = 203;
                return Content("Job item can't be removed");
            }
        }
    }
}