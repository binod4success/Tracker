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
    public class JobController : ApiController
    {
        private static readonly IJob _repos = new JobRepository();

        // GET api/job
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/job/5
        public Job Get(string jobId)
        {
            return _repos.GetJob(jobId);
        }

        // POST api/job
        public void Post(Job value)
        {
            _repos.InsertJob(value);
        }

        // PUT api/job/5
        public void Put(int jobId, Job value)
        {
            _repos.UpdateJob(value);
        }

        // DELETE api/job/5
        public void Delete(string jobId)
        {
            _repos.DeleteJob(jobId);
        }
    }
}
