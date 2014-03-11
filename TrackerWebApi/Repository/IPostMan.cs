using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerWebApi.Models;

namespace TrackerWebApi.Repository
{
    interface IPostMan
    {
        PostMan GetPostManInfo(string postManId);

        void InsertPostManInfo(PostMan postMan);

        void UpdatePostManInfo(PostMan postMan);

        void DeletePostManInfo(string postManId);
    }
}
