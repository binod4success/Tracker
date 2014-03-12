﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Tracker.Models;

namespace Tracker.Repository
{
    public class TrackingRepository : ITrackingDetails
    {
        public IList<Consignment> GetAllConsignmentDetails()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54865/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = client.GetAsync("api/Consignment").Result;
                if (response.IsSuccessStatusCode)
                {
                    IList<Consignment> info = response.Content.ReadAsAsync<IList<Consignment>>().Result;
                    return info;
                }
                else
                {
                    throw new Exception(string.Format("Error-Status Code: {0}. {1}", response.StatusCode, response.ReasonPhrase));
                }
            }
        }

        public IList<TrackingDetails> GetTrackingDetails(string trackingId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54865/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = client.GetAsync(string.Format("api/Tracking?trackingId={0}", trackingId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    IList<TrackingDetails> info = response.Content.ReadAsAsync<IList<TrackingDetails>>().Result;
                    return info;
                }
                else
                {
                    throw new Exception(string.Format("Error-Status Code: {0}. {1}", response.StatusCode, response.ReasonPhrase));
                }
            }

        }
    }
}