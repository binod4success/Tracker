using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Models;

namespace Tracker.Repository
{
    interface IPostMan
    {
        IList<PostMan> GetPostManList();

        PostMan GetPostManInfo(string postManId);

        void InsertPostManInfo(PostMan postMan);

        void UpdatePostManInfo(PostMan postMan);

        void DeletePostManInfo(string postManId);
    }
}
