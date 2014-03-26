using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TrackerWebApi.Repository
{
    public static class SqlHelper
    {
        public static void AddParameter(ref SqlCommand cmd, string parameterName, SqlDbType dataType, object value)
        {
            SqlParameter param = cmd.Parameters.Add(parameterName, dataType);
            if (value == null || (value is string && string.IsNullOrWhiteSpace(value.ToString())))
            {
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }
        }       
    }
}