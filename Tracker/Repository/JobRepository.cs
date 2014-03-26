using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Tracker.Models;

namespace Tracker.Repository
{
    public class JobRepository : IJob
    {
        public IList<Job> GetJobList()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54865/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET api/Tracking/Get?trackingId=Track1
                HttpResponseMessage response = client.GetAsync("api/Job/Get").Result;
                if (response.IsSuccessStatusCode)
                {
                    IList<Job> info = response.Content.ReadAsAsync<IList<Job>>().Result;
                    return info;
                }
                else
                {
                    throw new Exception(string.Format("Error-Status Code: {0}. {1}", response.StatusCode, response.ReasonPhrase));
                }
            }
        }

        public Job GetJob(string jobId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54865/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET api/Tracking/Get?trackingId=Track1
                HttpResponseMessage response = client.GetAsync(string.Format("api/Job/Get?JobId={0}", jobId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    Job info = response.Content.ReadAsAsync<Job>().Result;
                    return info;
                }
                else
                {
                    throw new Exception(string.Format("Error-Status Code: {0}. {1}", response.StatusCode, response.ReasonPhrase));
                }
            }
        }

        public bool DeleteJob(string jobId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54865/");
                client.DefaultRequestHeaders.Accept.Clear();
                var response = client.DeleteAsync(string.Format("api/Job/Delete?jobId={0}", jobId)).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(string.Format("Error-Status Code: {0}. {1}", response.StatusCode, response.ReasonPhrase));
                }
                else
                {
                    return true;
                }
            }
        }

        public void UpdateJob(Job data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54865/");
                client.DefaultRequestHeaders.Accept.Clear();
                var response = client.PutAsJsonAsync("api/Job/Put", data).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(string.Format("Error-Status Code: {0}. {1}", response.StatusCode, response.ReasonPhrase));
                }
            }
        }
    }
}