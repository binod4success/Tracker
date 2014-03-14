using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TrackerWebApi.Models;

namespace TrackerWebApi.Repository
{
    public class TrackingRepository : ITrack
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public void GetStatus(string trackingId)
        {
            throw new NotImplementedException();
        }

        public GeoLocation GetCurrentLocation(string trackingId)
        {
            throw new NotImplementedException();
        }

        public GeoLocation UpdateLocation(string trackingId, GeoLocation location)
        {
            throw new NotImplementedException();
        }

        public IList<TrackingDetails> GetTrackingDetails(string trackingId)
        {
            if (string.IsNullOrWhiteSpace(trackingId))
            {
                throw new ArgumentNullException("trackingId");
            }
            var QUERY = @"
                 SELECT TrackingDetails.Longitude, 
                        TrackingDetails.Latitude, 
                        TrackingDetails.JobId, 
                        TrackingDetails.SequenceId,
                        TrackingDetails.TrackingDateTime
                   FROM TrackingDetails
             INNER JOIN Job ON TrackingDetails.JobId = Job.JobId 
             INNER JOIN Consignment ON Job.ConsignmentId = Consignment.ConsignmentId
                  WHERE Consignment.TrackingId = @TrackingId";
            var locations = new List<TrackingDetails>();
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(QUERY, _dbConnection);
                cmd.Parameters.Add("@TrackingId", SqlDbType.VarChar).Value = trackingId;
                _dbConnection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var data = new TrackingDetails
                    {
                        JobId = Int32.Parse(reader["JobId"].ToString()),
                        TrackSequence = Int32.Parse(reader["SequenceId"].ToString()),
                        Locations = new GeoLocation
                        {
                            Latitude = reader["Latitude"].ToString(),
                            Longitude = reader["Longitude"].ToString()
                        }
                    };

                    if (!reader.IsDBNull(reader.GetOrdinal("TrackingDateTime")))
                    {
                        data.TrackingDateTime = DateTime.Parse(reader["TrackingDateTime"].ToString());
                    }
                    locations.Add(data);
                }
            }
            return locations;
        }
    }
}