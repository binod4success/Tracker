using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tracker.Models;
using Tracker.Repository;

namespace Tracker.ViewModels
{
    public class IndexViewModel
    {
        public IList<Consignment> ConsignmentList { get; set; }

        public string GetJobStatus(string statusId)
        {
            return RepositoryHelper.GetJobStatus(Int32.Parse(statusId));
        }
    }
}