using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Tracker.Models;
using System.Threading.Tasks;
using Tracker.Repository;

namespace Tracker
{
    public class TrackingHub : Hub
    {
        private static readonly ITrackingDetails _repos = new TrackingRepository();
        private static readonly IJob _jobRepos = new JobRepository();

        public static void Show(TrackingDetails data)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<TrackingHub>();
            context.Clients.Group("PostMan").displayStatus(data);
        }

        public void Send(string jobId, string lat, string lng)
        {
            var job = _jobRepos.GetJob(jobId);
            _repos.UpdateLocation(new TrackingDetails
            {
                JobId = Int32.Parse(jobId),
                TrackingDateTime = DateTime.Now,
                Locations = new GeoLocation
                {
                    Latitude = lat,
                    Longitude = lng
                }
            });
            // Call the broadcastMessage method to update clients.
            Clients.Group(job.Consignment.TrackingId).displayStatus(new { Latitude = lat, Longitude = lng });
        }

        public Task JoinGroup(string groupName)
        {
            return Groups.Add(Context.ConnectionId, groupName);
        }

        public Task LeaveGroup(string groupName)
        {
            return Groups.Remove(Context.ConnectionId, groupName);
        }
    }
}