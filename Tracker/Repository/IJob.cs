using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Models;

namespace Tracker.Repository
{
    interface IJob
    {
        IList<Job> GetJobList();

        Job GetJob(string jobId);

        void UpdateJob(Job data);

        bool DeleteJob(string jobId);
    }
}
