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
    public class AddressRepository : IAddress
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public Address GetAddress(string addressId)
        {
            if (string.IsNullOrWhiteSpace(addressId))
            {
                throw new ArgumentNullException("addressId");
            }
            var QUERY = @"
                SELECT [AddressId]
                       [AddressLine1],
                       [AddressLine2],
                       [LandMark],
                       [City],
                       [State],
                       [Country],
                       [PinCode],
                       [Latitude],
                       [Longitude],
                  FROM [Tracker].[dbo].[Address]
                 WHERE [AddressId] = @AddressId ";
            var addresses = new List<Address>();
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(QUERY, _dbConnection);
                cmd.Parameters.Add("@AddressId", SqlDbType.Int).Value = addressId;
                _dbConnection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var data = new Address
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
                    };
                    addresses.Add(data);
                }
            }
            return addresses.FirstOrDefault();
        }

        public int InsertAddress(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }
            var QUERY = @"
                INSERT INTO [Tracker].[dbo].[Address]
                       ([AddressLine1],
                        [AddressLine2],
                        [LandMark],
                        [City],
                        [State],
                        [Country],
                        [PinCode],
                        [Latitude],
                        [Longitude])
                 VALUES
                       (@AddressLine1,
                        @AddressLine2,
                        @LandMark,
                        @City,
                        @State,
                        @Country,
                        @PinCode,
                        @Latitude,
                        @Longitude);

                SELECT SCOPE_IDENTITY();
                GO";

            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(QUERY, _dbConnection);

                SqlHelper.AddParameter(ref cmd, "@AddressLine1", SqlDbType.VarChar, address.AddressLine1);
                SqlHelper.AddParameter(ref cmd, "@AddressLine2", SqlDbType.VarChar, address.AddressLine2);
                SqlHelper.AddParameter(ref cmd, "@LandMark", SqlDbType.VarChar, address.LandMark);
                SqlHelper.AddParameter(ref cmd, "@City", SqlDbType.VarChar, address.City);
                SqlHelper.AddParameter(ref cmd, "@State", SqlDbType.VarChar, address.State);
                SqlHelper.AddParameter(ref cmd, "@Country", SqlDbType.VarChar, address.Country);
                SqlHelper.AddParameter(ref cmd, "@PinCode", SqlDbType.VarChar, address.PinCode);
                SqlHelper.AddParameter(ref cmd, "@Latitude", SqlDbType.Decimal, address.GeoLocation.Latitude);
                SqlHelper.AddParameter(ref cmd, "@Longitude", SqlDbType.Decimal, address.GeoLocation.Longitude);

                _dbConnection.Open();
                var AddressId = Int32.Parse(cmd.ExecuteScalar().ToString());
                return AddressId;
            }
        }

        public void UpdateAddress(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                string QUERY = @"
                         UPDATE [Tracker].[dbo].[Address]
                            SET [AddressLine1] = @AddressLine1,
                                [AddressLine2] = @AddressLine2,
                                [LandMark] = @LandMark,                                   
                                [City] = @City,
                                [State] = @State,
                                [Country] = @Country,
                                [PinCode] = @PinCode,
                                [Latitude] = @Latitude,
                                [Longitude] = @Longitude
                          WHERE [AddressId] = @AddressId ";

                SqlCommand cmd = new SqlCommand(QUERY, _dbConnection);
                SqlHelper.AddParameter(ref cmd, "@AddressId", SqlDbType.VarChar, address.AddressId);
                SqlHelper.AddParameter(ref cmd, "@AddressLine1", SqlDbType.VarChar, address.AddressLine1);
                SqlHelper.AddParameter(ref cmd, "@AddressLine2", SqlDbType.VarChar, address.AddressLine2);
                SqlHelper.AddParameter(ref cmd, "@LandMark", SqlDbType.VarChar, address.LandMark);
                SqlHelper.AddParameter(ref cmd, "@City", SqlDbType.VarChar, address.City);
                SqlHelper.AddParameter(ref cmd, "@State", SqlDbType.VarChar, address.State);
                SqlHelper.AddParameter(ref cmd, "@Country", SqlDbType.VarChar, address.Country);
                SqlHelper.AddParameter(ref cmd, "@PinCode", SqlDbType.VarChar, address.PinCode);
                SqlHelper.AddParameter(ref cmd, "@Latitude", SqlDbType.Decimal, address.GeoLocation.Latitude);
                SqlHelper.AddParameter(ref cmd, "@Longitude", SqlDbType.Decimal, address.GeoLocation.Longitude);

                _dbConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAddress(string addressId)
        {
            if (string.IsNullOrWhiteSpace(addressId))
            {
                throw new ArgumentNullException("addressId");
            }
            const string QUERY = @" [Tracker].[dbo].[Address] WHERE AddressId = @AddressId";
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(QUERY, _dbConnection);
                cmd.Parameters.Add("@AddressId", SqlDbType.Int).Value = addressId;

                _dbConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}