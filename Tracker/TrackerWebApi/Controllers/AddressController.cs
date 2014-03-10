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
    public class AddressController : ApiController
    {
        private static readonly IAddress _repos = new AddressRepository();

        // GET api/address
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/address/5
        public Address Get(string id)
        {
            return _repos.GetAddress(id);
        }

        // POST api/address
        public void Post(Address value)
        {
            _repos.InsertAddress(value);
        }

        // PUT api/address/5
        public void Put(string id, Address value)
        {
            _repos.UpdateAddress(value);
        }

        // DELETE api/address/5
        public void Delete(string id)
        {
            _repos.DeleteAddress(id);
        }
    }
}
