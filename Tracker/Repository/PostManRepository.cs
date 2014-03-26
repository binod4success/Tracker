using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Tracker.Models;

namespace Tracker.Repository
{
    public class PostManRepository : IPostMan
    {
        public PostMan GetPostManInfo(string postManId)
        {
            throw new NotImplementedException();
        }

        public IList<PostMan> GetPostManList()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54865/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = client.GetAsync("api/PostMan/Get").Result;
                if (response.IsSuccessStatusCode)
                {
                    IList<PostMan> info = response.Content.ReadAsAsync<IList<PostMan>>().Result;
                    return info;
                }
                else
                {
                    throw new Exception(string.Format("Error-Status Code: {0}. {1}", response.StatusCode, response.ReasonPhrase));
                }
            }
        }

        public void InsertPostManInfo(PostMan postMan)
        {
            throw new NotImplementedException();
        }

        public void UpdatePostManInfo(PostMan postMan)
        {
            throw new NotImplementedException();
        }

        public void DeletePostManInfo(string postManId)
        {
            throw new NotImplementedException();
        }
    }
}