using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Tracker.Models;
using Tracker.Repository;

namespace Tracker.ViewModels
{
    public class JobViewModel
    {
        public int JobId { get; set; }

        public int PostManId { get; set; }

        public IList<Job> TaskList { get; set; }

        public IList<SelectListItem> PostMen { get; set; }

        public int JobStatusId { get; set; }

        public IList<SelectListItem> JobStatusList
        {
            get
            {
                return RepositoryHelper.GetJobStatusList();
            }
        }

        public string Status(string statusId)
        {
            return RepositoryHelper.GetJobStatus(Int32.Parse(statusId));
        }
    }
}