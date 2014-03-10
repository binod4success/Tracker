using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerWebApi.Models;

namespace TrackerWebApi.Repository
{
    interface ITrack
    {
        void GetStatus(string trackingId);

        GeoLocation GetCurrentLocation(string trackingId);

        GeoLocation UpdateLocation(string trackingId, GeoLocation location);
    }
}
