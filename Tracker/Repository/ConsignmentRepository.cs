using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Tracker.Models;

namespace Tracker.Repository
{
    public class ConsignmentRepository : IConsignment
    {
        public Consignment GetConsignment(int? consignmentId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54865/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = client.GetAsync(string.Format("api/Tracking/Get?trackingId={0}", consignmentId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    Consignment info = response.Content.ReadAsAsync<Consignment>().Result;
                    return info;
                }
                else
                {
                    throw new Exception(string.Format("Error-Status Code: {0}. {1}", response.StatusCode, response.ReasonPhrase));
                }
            }
        }

        public void InsertConsignment(Consignment consignment)
        {
            // HTTP POST
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54865/");
                client.DefaultRequestHeaders.Accept.Clear();
                var response = client.PostAsJsonAsync("api/Consignment/Post", consignment).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(string.Format("Error-Status Code: {0}. {1}", response.StatusCode, response.ReasonPhrase));
                }
            }
        }

        public void UpdateConsignment(Models.Consignment consignment)
        {
            // HTTP POST
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54865/");
                client.DefaultRequestHeaders.Accept.Clear();
                var response = client.PutAsJsonAsync("api/Consignment/Put", consignment).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(string.Format("Error-Status Code: {0}. {1}", response.StatusCode, response.ReasonPhrase));
                }
            }
        }

        public void DeleteConsignment(string consignmentId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54865/");
                client.DefaultRequestHeaders.Accept.Clear();
                var response = client.DeleteAsync(string.Format("api/Consignment/Delete?id={0}", consignmentId)).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(string.Format("Error-Status Code: {0}. {1}", response.StatusCode, response.ReasonPhrase));
                }
            }
        }
    }
}