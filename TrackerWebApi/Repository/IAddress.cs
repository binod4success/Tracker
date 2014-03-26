using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerWebApi.Models;

namespace TrackerWebApi.Repository
{
    interface IAddress
    {
        Address GetAddress(string addressId);

        int? InsertAddress(Address address);

        void UpdateAddress(Address address);

        void DeleteAddress(string addressId);
    }
}
