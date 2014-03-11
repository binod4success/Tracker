using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tracker.Models;

namespace Tracker.ViewModels
{
    public class IndexViewModel
    {
        public IList<Consignment> ConsignmentList { get; set; }
    }
}