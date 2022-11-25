using CommonLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
                    SqlCommand cmd = new SqlCommand("SP_AddFeedback", sqlConnection);
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
        public List<GetFeedbackDataModel> GetAllFeedbacks(int bookId)
        {
            sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:EBookStore"]);
            using (sqlConnection)
            {
                try
                {
                    List<GetFeedbackDataModel> feedbackModel = new List<GetFeedbackDataModel>();
                    SqlCommand cmd = new SqlCommand("SP_GetAllFeedback", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    sqlConnection.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            GetFeedbackDataModel feedback = new GetFeedbackDataModel();
                            feedback.FeedbackId = Convert.ToInt32(rdr["FeedbackId"]);
                            feedback.BookId = Convert.ToInt32(rdr["BookId"]);
                            feedback.UserId = Convert.ToInt32(rdr["UserId"]);
                            feedback.Comment = Convert.ToString(rdr["Comment"]);
                            feedback.Rating = Convert.ToInt32(rdr["Rating"]);
                            feedback.FullName = Convert.ToString(rdr["FullName"]);
                            feedbackModel.Add(feedback);
                        }
                        sqlConnection.Close();
                        return feedbackModel;
                    }
                    else
                    {
                        sqlConnection.Close();
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}
