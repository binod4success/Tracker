using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace Tracker.Repository
{
    public static class RepositoryHelper
    {
        public static string GenerateRandomId()
        {
            string strCharSet = "abcdefghijklmnopqrstuvwxyz0123456789#@&$ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string strRandom = "";
            Random rnd = new Random();
            for (int i = 0; i <= 7; i++)
            {
                int iRandom = rnd.Next(0, strCharSet.Length - 1);
                strRandom += strCharSet.Substring(iRandom, 1);
            }
            return strRandom;
        }

        public static string GetJobStatus(int statusId)
        {
            return Enum.Parse(typeof(StatusEnum), statusId.ToString()).ToString();
        }

        internal enum StatusEnum
        {
            New = 0,

            Inprogress = 1,

            Delivered = 2,

            Hold = 3,

            Cancel = 4
        }

        internal static IList<System.Web.Mvc.SelectListItem> GetJobStatusList()
        {
            return System.Web.Mvc.Html.EnumHelper.GetSelectList(typeof(StatusEnum)); ;
        }
    }
}