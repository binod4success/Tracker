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
    public class JobRepository : IJob
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public Job GetJob(string jobId)
        {
            if (string.IsNullOrWhiteSpace(jobId))
            {
                throw new ArgumentNullException("jobId");
            }
            var QUERY = @"
                SELECT [JobId],
                       [ConsignmentId],
                       [JobName],
                       [PostManId],
                       [Status]
                  FROM [Tracker].[dbo].[Job]
                 WHERE [JobId] = @JobId ";
            var jobs = new List<Job>();
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(QUERY, _dbConnection);
                cmd.Parameters.Add("@JobId", SqlDbType.Int).Value = jobId;
                _dbConnection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var data = new Job
                    {
                        JobId = Int32.Parse(reader["JobId"].ToString()),
                        ConsignmentId = Int32.Parse(reader["ConsignmentId"].ToString()),
                        JobName = reader["JobName"].ToString(),
                        PostManId = Int32.Parse(reader["PostManId"].ToString()),
                        Status = reader["Status"].ToString()
                    };
                    jobs.Add(data);
                }
            }
            return jobs.FirstOrDefault();
        }

        public void InsertJob(Job job)
        {
            if (job == null)
            {
                throw new ArgumentNullException("job");
            }
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                var QUERY = @"
                           INSERT INTO [Tracker].[dbo].[Job]
                                      ([ConsignmentId],
                                       [JobName],
                                       [PostManId],
                                       [Status])
                               VALUES
                                      (@ConsignmentId,
                                       @JobName,
                                       @PostManId,
                                       @Status)";

                SqlCommand cmd = new SqlCommand(QUERY, _dbConnection);
                                
                SqlHelper.AddParameter(ref cmd, "@ConsignmentId", SqlDbType.Int, job.ConsignmentId);
                SqlHelper.AddParameter(ref cmd, "@JobName", SqlDbType.VarChar, job.JobName);
                SqlHelper.AddParameter(ref cmd, "@PostManId", SqlDbType.Int, job.PostManId);
                SqlHelper.AddParameter(ref cmd, "@Status", SqlDbType.VarChar, job.Status);

                _dbConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateJob(Job job)
        {
            if (job == null)
            {
                throw new ArgumentNullException("Job");
            }
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                string QUERY = @"
                            UPDATE [Tracker].[dbo].[Job]
                               SET [ConsignmentId] = @ConsignmentId,
                                   [JobName] = @JobName,                                   
                                   [PostManId] = @PostManId,
                                   [Status] = @Status
                             WHERE [JobId] = @JobId";

                SqlCommand cmd = new SqlCommand(QUERY, _dbConnection);

                SqlHelper.AddParameter(ref cmd, "@JobId", SqlDbType.Int, job.JobId);
                SqlHelper.AddParameter(ref cmd, "@ConsignmentId", SqlDbType.Int, job.ConsignmentId);
                SqlHelper.AddParameter(ref cmd, "@JobName", SqlDbType.VarChar, job.JobName);
                SqlHelper.AddParameter(ref cmd, "@PostManId", SqlDbType.Int, job.PostManId);
                SqlHelper.AddParameter(ref cmd, "@Status", SqlDbType.VarChar, job.Status);

                _dbConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteJob(string jobId)
        {
            const string QUERY = @" [Tracker].[dbo].[Job] WHERE JobId = @jobId";
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(QUERY, _dbConnection);
                cmd.Parameters.Add("@JobId", SqlDbType.Int).Value = jobId;

                _dbConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}