﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TrackerWebApi.Models;

namespace TrackerWebApi.Repository
{
    public class ConsignmentRepository : IConsignment
    {
        private static readonly IAddress _addressRepos = new AddressRepository();

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private List<Consignment> GetConsignmentDetails(int? consignmentId)
        {
            string QUERY = @"SELECT Consignment.ConsignmentId       AS ConsignmentId,
                                    Consignment.TrackingId          AS TrackingId,
                                    Consignment.Status              AS Status,
                                    Consignment.Remarks             AS Remarks,
                                    Consignment.ContactNumber       AS ContactNumber,
                                    Consignment.AssignDateTime      AS AssignDateTime,
                                    Consignment.DeliveryDateTime    AS DeliveryDateTime,
                                    Address.AddressId               AS AddressId,
                                    Address.AddressLine1            AS AddressLine1,
                                    Address.AddressLine2            AS AddressLine2,
                                    Address.LandMark                AS LandMark,
                                    Address.City                    AS City,
                                    Address.State                   AS State,
                                    Address.Country                 AS Country,
                                    Address.PinCode                 AS PinCode,
                                    Address.Latitude                AS Latitude,
                                    Address.Longitude               AS Longitude
                               FROM [Tracker].[dbo].[Consignment]
                              INNER JOIN [Tracker].[dbo].[Address]
                                 ON Consignment.AddressId = Address.AddressId";
            var consignments = new List<Consignment>();
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _dbConnection;
                if (consignmentId.HasValue)
                {
                    cmd.Parameters.Add("@ConsignmentId", SqlDbType.Int).Value = consignmentId.Value;
                    QUERY += " WHERE [ConsignmentId] = @ConsignmentId";
                }
                cmd.CommandText = QUERY;
                _dbConnection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var data = new Consignment
                    {
                        ConsignmentId = Int32.Parse(reader["ConsignmentId"].ToString()),
                        TrackingId = reader["TrackingId"].ToString(),
                        Status = reader["Status"].ToString(),
                        Remarks = reader["Remarks"].ToString(),
                        ContactNumber = reader["ContactNumber"].ToString(),
                        Destination = new Address
                        {
                            AddressId = Int32.Parse(reader["AddressId"].ToString()),
                            AddressLine1 = reader["AddressLine1"].ToString(),
                            AddressLine2 = reader["AddressLine2"].ToString(),
                            LandMark = reader["LandMark"].ToString(),
                            City = reader["City"].ToString(),
                            State = reader["State"].ToString(),
                            Country = reader["Country"].ToString(),
                            PinCode = reader["PinCode"].ToString(),
                            GeoLocation = new GeoLocation
                            {
                                Latitude = reader["Latitude"].ToString(),
                                Longitude = reader["Longitude"].ToString()
                            }
                        }
                    };
                    if (!reader.IsDBNull(reader.GetOrdinal("AssignDateTime")))
                    {
                        data.AssignDateTime = DateTime.Parse(reader["AssignDateTime"].ToString());
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("DeliveryDateTime")))
                    {
                        data.DeliveryDateTime = DateTime.Parse(reader["DeliveryDateTime"].ToString());
                    }
                    consignments.Add(data);
                }
            }
            return consignments;
        }

        public List<Consignment> GetConsignments()
        {
            return GetConsignmentDetails(null);
        }

        public Consignment GetConsignment(int? consignmentId)
        {
            if (!consignmentId.HasValue)
            {
                throw new ArgumentNullException("consignmentId");
            }
            var consignments = GetConsignmentDetails(consignmentId);
            return consignments.FirstOrDefault();
        }

        public void InsertConsignment(Consignment consignment)
        {
            if (consignment == null)
            {
                throw new ArgumentNullException("consignment");
            }
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                try
                {
                    var addressId = _addressRepos.InsertAddress(consignment.Destination);
                    if (addressId == null)
                    {
                        throw new Exception("Error: Address couldn't be updated now.");
                    }
                    var QUERY = @"
                           INSERT INTO [Tracker].[dbo].[Consignment]
                                      ([TrackingId],
                                       [Status],
                                       [Remarks],
                                       [AddressId],
                                       [ContactNumber],
                                       [AssignDateTime],
                                       [DeliveryDateTime])
                               VALUES
                                      (@TrackingId,
                                       @Status,
                                       @Remarks,
                                       @AddressId,
                                       @ContactNumber,
                                       @AssignDateTime,
                                       @DeliveryDateTime)";

                    SqlCommand cmd = new SqlCommand(QUERY, _dbConnection);

                    SqlHelper.AddParameter(ref cmd, "@TrackingId", SqlDbType.VarChar, consignment.TrackingId);
                    SqlHelper.AddParameter(ref cmd, "@Status", SqlDbType.VarChar, consignment.Status);
                    SqlHelper.AddParameter(ref cmd, "@Remarks", SqlDbType.VarChar, consignment.Remarks);
                    SqlHelper.AddParameter(ref cmd, "@AddressId", SqlDbType.Int, addressId);
                    SqlHelper.AddParameter(ref cmd, "@ContactNumber", SqlDbType.VarChar, consignment.ContactNumber);
                    SqlHelper.AddParameter(ref cmd, "@AssignDateTime", SqlDbType.DateTime, consignment.AssignDateTime);
                    SqlHelper.AddParameter(ref cmd, "@DeliveryDateTime", SqlDbType.DateTime, consignment.DeliveryDateTime);
                    _dbConnection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void UpdateConsignment(Consignment consignment)
        {
            if (consignment == null)
            {
                throw new ArgumentNullException("consignment");
            }
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                try
                {
                    _addressRepos.UpdateAddress(consignment.Destination);

                    string QUERY = @"
                            UPDATE [Tracker].[dbo].[Consignment]
                               SET [TrackingId] = @TrackingId,
                                   [Status] = @Status,
                                   [Remarks] = @Remarks,                                   
                                   [ContactNumber] = @ContactNumber,
                                   [AssignDateTime] = @AssignDateTime,
                                   [DeliveryDateTime] = @DeliveryDateTime
                             WHERE [ConsignmentId] = @ConsignmentId";

                    SqlCommand cmd = new SqlCommand(QUERY, _dbConnection);

                    SqlHelper.AddParameter(ref cmd, "@TrackingId", SqlDbType.VarChar, consignment.TrackingId);
                    SqlHelper.AddParameter(ref cmd, "@Status", SqlDbType.VarChar, consignment.Status);
                    SqlHelper.AddParameter(ref cmd, "@Remarks", SqlDbType.VarChar, consignment.Remarks);
                    SqlHelper.AddParameter(ref cmd, "@ContactNumber", SqlDbType.VarChar, consignment.ContactNumber);
                    SqlHelper.AddParameter(ref cmd, "@AssignDateTime", SqlDbType.DateTime, consignment.AssignDateTime);
                    SqlHelper.AddParameter(ref cmd, "@DeliveryDateTime", SqlDbType.DateTime, consignment.DeliveryDateTime);
                    SqlHelper.AddParameter(ref cmd, "@ConsignmentId", SqlDbType.Int, consignment.ConsignmentId);

                    _dbConnection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void DeleteConsignment(string consignmentId)
        {
            const string QUERY = @" DELETE FROM [Tracker].[dbo].[Consignment] WHERE [ConsignmentId] = @ConsignmentId";
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(QUERY, _dbConnection);
                cmd.Parameters.Add("@ConsignmentId", SqlDbType.Int).Value = consignmentId;

                _dbConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}