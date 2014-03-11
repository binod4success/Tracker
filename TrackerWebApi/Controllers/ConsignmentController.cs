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
    public class ConsignmentController : ApiController
    {
        private static readonly IConsignment _repos = new ConsignmentRepository();

        // GET api/values
        public IList<Consignment> Get()
        {
            return _repos.GetConsignments();
        }

        // GET api/values/5
        public Consignment Get(int? id)
        {
            return _repos.GetConsignment(id);
        }

        // POST api/values
        public void Post(Consignment value)
        {
            _repos.InsertConsignment(value);
        }

        // PUT api/values/5
        public void Put(int id, Consignment value)
        {
            _repos.UpdateConsignment(value);
        }

        // DELETE api/values/5
        public void Delete(string id)
        {
            _repos.DeleteConsignment(id);
        }
    }
}