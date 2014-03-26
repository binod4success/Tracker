using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackerWebApi.Models
{
    public class Job
    {
        public int JobId { get; set; }

        public string JobName { get; set; }

        public Consignment Consignment { get; set; }

        public PostMan PostMan { get; set; }

        public int StatusId { get; set; }
    }
}