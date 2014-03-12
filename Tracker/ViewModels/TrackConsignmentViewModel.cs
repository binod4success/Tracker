using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tracker.Models;

namespace Tracker.ViewModels
{
    public class TrackConsignmentViewModel
    {
        public string TrackingId { get; set; }

        public IList<TrackingDetails> TrackingLocations { get; set; }
    }
}