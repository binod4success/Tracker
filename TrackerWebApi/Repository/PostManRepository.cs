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
    public class PostManRepository : IPostMan
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private IList<PostMan> _getPostManList(string PostManId)
        {
            var QUERY = @"
                SELECT [PostManId],
                       [ContactNumber],
                       [Name]
                  FROM [Tracker].[dbo].[PostMan]";

            var postMen = new List<PostMan>();
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _dbConnection;
                if (!string.IsNullOrWhiteSpace(PostManId))
                {
                    QUERY += " WHERE [PostManId] = @PostManId";
                    cmd.Parameters.Add("@PostManId", SqlDbType.Int).Value = PostManId;
                }
                cmd.CommandText = QUERY;
                _dbConnection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var data = new PostMan
                    {
                        PostManId = Int32.Parse(reader["PostManId"].ToString()),
                        ContactNumber = reader["ContactNumber"].ToString(),
                        Name = reader["Name"].ToString()
                    };
                    postMen.Add(data);
                }
            }
            return postMen;
        }

        public IList<PostMan> GetPostManList()
        {
            return _getPostManList(null);
        }

        public PostMan GetPostManInfo(string PostManId)
        {
            if (string.IsNullOrWhiteSpace(PostManId))
            {
                throw new ArgumentNullException("PostManId");
            }
            return _getPostManList(PostManId).FirstOrDefault();
        }

        public void InsertPostManInfo(PostMan PostMan)
        {
            if (PostMan == null)
            {
                throw new ArgumentNullException("PostMan");
            }
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                var QUERY = @"
                           INSERT INTO [Tracker].[dbo].[PostMan]
                                      ([PostManId],
                                       [Name],
                                       [ContactNumber])
                               VALUES
                                      (@PostManId,
                                       @Name,
                                       @ContactNumber)";

                SqlCommand cmd = new SqlCommand(QUERY, _dbConnection);

                SqlHelper.AddParameter(ref cmd, "@PostManId", SqlDbType.Int, PostMan.PostManId);
                SqlHelper.AddParameter(ref cmd, "@ContactNumber", SqlDbType.Int, PostMan.ContactNumber);
                SqlHelper.AddParameter(ref cmd, "@Name", SqlDbType.VarChar, PostMan.Name);

                _dbConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdatePostManInfo(PostMan PostMan)
        {
            if (PostMan == null)
            {
                throw new ArgumentNullException("PostMan");
            }
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                string QUERY = @"
                            UPDATE [Tracker].[dbo].[PostMan]
                               SET [ContactNumber] = @ContactNumber,
                                   [Name] = @Name
                             WHERE [PostManId] = @PostManId";

                SqlCommand cmd = new SqlCommand(QUERY, _dbConnection);

                SqlHelper.AddParameter(ref cmd, "@PostManId", SqlDbType.Int, PostMan.PostManId);
                SqlHelper.AddParameter(ref cmd, "@ContactNumber", SqlDbType.Int, PostMan.ContactNumber);
                SqlHelper.AddParameter(ref cmd, "@Name", SqlDbType.VarChar, PostMan.Name);

                _dbConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePostManInfo(string PostManId)
        {
            const string QUERY = @" [Tracker].[dbo].[PostMan] WHERE PostManId = @PostManId";
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(QUERY, _dbConnection);
                cmd.Parameters.Add("@PostManId", SqlDbType.Int).Value = PostManId;

                _dbConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}