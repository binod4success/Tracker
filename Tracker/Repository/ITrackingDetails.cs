using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Models;

namespace Tracker.Repository
{
    interface ITrackingDetails
    {
        IList<Consignment> GetAllConsignmentDetails();

        IList<TrackingDetails> GetTrackingDetails(string trackingId);

        TrackingDetails GetCurrentLocation(string trackingId);
        
        void UpdateLocation(TrackingDetails trackingDetails);
    }
}
