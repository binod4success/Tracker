using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tracker.Models;
using Tracker.Repository;

namespace Tracker.ViewModels
{
    public class TrackerViewModel
    {        
        public IList<Job> TaskList { get; set; }
        
        public string GetJobStatus(string statusId)
        {
            return RepositoryHelper.GetJobStatus(Int32.Parse(statusId));
        }
    }
}