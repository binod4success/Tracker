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
    public class PostManController : ApiController
    {
        private static readonly IPostMan _repos = new PostManRepository();

        // GET api/postman
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/postman/5
        public PostMan Get(string postManId)
        {
            return _repos.GetPostManInfo(postManId);
        }

        // POST api/postman
        public void Post(PostMan value)
        {
            _repos.InsertPostManInfo(value);
        }

        // PUT api/postman/5
        public void Put(string postManId, PostMan value)
        {
            _repos.UpdatePostManInfo(value);
        }

        // DELETE api/postman/5
        public void Delete(string postManId)
        {
            _repos.DeletePostManInfo(postManId);
        }
    }
}
