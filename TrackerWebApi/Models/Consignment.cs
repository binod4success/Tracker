using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackerWebApi.Models
{
    public class Consignment
    {
        public int ConsignmentId { get; set; }

        public string TrackingId { get; set; }

        public string Status { get; set; }

        public string Remarks { get; set; }
        
        public Address Destination { get; set; }

        public string ContactNumber { get; set; }

        public DateTime? AssignDateTime { get; set; }

        public DateTime? DeliveryDateTime { get; set; }
    }
}