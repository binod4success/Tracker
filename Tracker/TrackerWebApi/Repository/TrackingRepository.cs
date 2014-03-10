using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackerWebApi.Models;

namespace TrackerWebApi.Repository
{
    public class TrackingRepository :ITrack
    {
        public void GetStatus(string trackingId)
        {
            throw new NotImplementedException();
        }

        public GeoLocation GetCurrentLocation(string trackingId)
        {
            throw new NotImplementedException();
        }

        public GeoLocation UpdateLocation(string trackingId, GeoLocation location)
        {
            throw new NotImplementedException();
        }
    }
}