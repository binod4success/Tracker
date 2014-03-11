using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerWebApi.Models;

namespace TrackerWebApi.Repository
{
    interface IJob
    {
        Job GetJob(string jobId);

        void InsertJob(Job job);

        void UpdateJob(Job job);

        void DeleteJob(string jobId);
    }
}
