using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrackerWebApi.Models;
using TrackerWebApi.Repository;

namespace TrackerWebApi.Controllers
{
    public class TrackingController : ApiController
    {
        private static readonly ITrack _repos = new TrackingRepository();

        // GET api/tracking
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/tracking/5
        public IList<TrackingDetails> Get(string trackingId)
        {
            return _repos.GetTrackingDetails(trackingId);
        }

        // POST api/tracking
        public void Post(TrackingDetails value)
        {
        }

        // PUT api/tracking/5
        public void Put(string trackingId, TrackingDetails value)
        {
        }

        // DELETE api/tracking/5
        public void Delete(string trackingId)
        {
        }
    }
}
