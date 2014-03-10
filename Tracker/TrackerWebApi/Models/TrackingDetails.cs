using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackerWebApi.Models
{
    public class TrackingDetails
    {
        public int JobId { get; set; }

        public IList<GeoLocation> Locations { get; set; }
    }
}