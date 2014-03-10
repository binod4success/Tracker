using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerWebApi.Models;

namespace TrackerWebApi.Repository
{
    interface IConsignment
    {
        Consignment GetConsignment(string consignmentId);

        void InsertConsignment(Consignment consignment);

        void UpdateConsignment(Consignment consignment);

        void DeleteConsignment(string consignmentId);
    }
}
