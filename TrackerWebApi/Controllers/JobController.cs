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

        // GET api/job/Get
        public IEnumerable<Job> Get()
        {
            var data = _repos.GetJobs();
            return data;
        }

        // GET api/job/Get?id=5
        public Job Get(string jobId)
        {
            return _repos.GetJob(jobId);
        }

        // POST api/job/Post
        public void Post(Job value)
        {
            _repos.InsertJob(value);
        }

        // PUT api/job/Put
        public void Put(Job value)
        {
            _repos.UpdateJob(value);
        }

        // DELETE api/job/Delete?id=5
        public void Delete(string jobId)
        {
            _repos.DeleteJob(jobId);
        }
    }
}
