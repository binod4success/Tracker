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

        public int ConsignmentId { get; set; }

        public int PostManId { get; set; }

        public string Status { get; set; }
    }
}