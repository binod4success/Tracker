using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracker.Models;
using Tracker.Repository;

namespace Tracker.ViewModels
{
    public class ConsignmentViewModel
    {
        public string Mode { get; set; }

        public IList<Consignment> ConsignmentList { get; set; }

        public Consignment NewConsignment { get; set; }

        public string GetJobStatus(string statusId)
        {
            return RepositoryHelper.GetJobStatus(Int32.Parse(statusId));
        }

        public IList<SelectListItem> JobStatusList
        {
            get
            {
                return RepositoryHelper.GetJobStatusList();
            }
        }
    }
}