using CommonLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FeedbackRL:IFeedbackRL
    {
        private SqlConnection sqlConnection;

        private IConfiguration configuration { get; }
        public FeedbackRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public FeedbackModel AddFeedback(FeedbackModel model, int userId)
        {
           sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:EBookStore"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("SP_Feedback", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Comment", model.Comment);
                    cmd.Parameters.AddWithValue("@Rating", model.Rating);
                    cmd.Parameters.AddWithValue("@BookId",model.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    return model;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
