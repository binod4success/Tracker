using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tracker.Models;
using Tracker.Repository;

namespace Tracker.ViewModels
{
    public class TrackMeViewModel
    {
        public int JobId { get; set; }

        public PostMan Me { get; set; }

        public Consignment Consignment { get; set; }
        
        public string GetJobStatus(string statusId)
        {
            return RepositoryHelper.GetJobStatus(Int32.Parse(statusId));
        }
    }
}