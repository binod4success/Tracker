using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Models;

namespace Tracker.Repository
{
    interface IConsignment
    {
        Consignment GetConsignment(int? consignmentId);

        void InsertConsignment(Consignment consignment);

        void UpdateConsignment(Consignment consignment);

        void DeleteConsignment(string consignmentId);
    }
}
