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
        private static readonly IConsignment _consignmentRepos = new ConsignmentRepository();
        private static readonly IPostMan _postManRepos = new PostManRepository();

        private IList<Job> GetJobsList(string jobId)
        {
            var QUERY = @"
                SELECT [JobId],
                       [ConsignmentId],
                       [JobName],
                       [PostManId],
                       [Status]
                  FROM [Tracker].[dbo].[Job]";

            var jobs = new List<Job>();
            using (SqlConnection _dbConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _dbConnection;
                if (!string.IsNullOrWhiteSpace(jobId))
                {
                    QUERY += " WHERE [JobId] = @JobId ";
                    cmd.Parameters.Add("@JobId", SqlDbType.Int).Value = jobId;
                }
                cmd.CommandText = QUERY;
                _dbConnection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var data = new Job
                    {
                        JobId = Int32.Parse(reader["JobId"].ToString()),
                        Consignment = _consignmentRepos.GetConsignment(Int32.Parse(reader["ConsignmentId"].ToString())),
                        PostMan = _postManRepos.GetPostManInfo(reader["PostManId"].ToString()),
                        JobName = reader["JobName"].ToString(),
                        StatusId = Int32.Parse(reader["Status"].ToString())
                    };
                    jobs.Add(data);
                }
            }
            return jobs;
        }

        public IList<Job> GetJobs()
        {
            return GetJobsList(null);
        }

        public Job GetJob(string jobId)
        {
            if (string.IsNullOrWhiteSpace(jobId))
            {
                throw new ArgumentNullException("jobId");
            }
            var jobs = GetJobsList(jobId);
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

                SqlHelper.AddParameter(ref cmd, "@ConsignmentId", SqlDbType.Int, job.Consignment.ConsignmentId);
                SqlHelper.AddParameter(ref cmd, "@JobName", SqlDbType.VarChar, job.JobName);
                SqlHelper.AddParameter(ref cmd, "@PostManId", SqlDbType.Int, job.PostMan.PostManId);
                SqlHelper.AddParameter(ref cmd, "@Status", SqlDbType.VarChar, job.StatusId);

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
                               SET [PostManId] = @PostManId,
                                   [Status] = @Status
                             WHERE [JobId] = @JobId";

                SqlCommand cmd = new SqlCommand(QUERY, _dbConnection);

                SqlHelper.AddParameter(ref cmd, "@JobId", SqlDbType.Int, job.JobId);                
                SqlHelper.AddParameter(ref cmd, "@PostManId", SqlDbType.Int, job.PostMan.PostManId);
                SqlHelper.AddParameter(ref cmd, "@Status", SqlDbType.VarChar, job.StatusId);

                _dbConnection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteJob(string jobId)
        {
            const string QUERY = @"DELETE FROM [Tracker].[dbo].[Job] WHERE [JobId] = @jobId";
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