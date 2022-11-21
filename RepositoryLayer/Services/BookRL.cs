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
    public class BookRL : IBookRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }

        public BookRL(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public BookModel CreateBook(BookModel bookModel)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
                SqlCommand cmd = new SqlCommand("dbo.SP_CreateBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@BookName", bookModel.BookName);
                cmd.Parameters.AddWithValue("@Author", bookModel.Author);
                cmd.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                cmd.Parameters.AddWithValue("@BookDetail", bookModel.BookDetail);
                cmd.Parameters.AddWithValue("@DiscountPrice", bookModel.DiscountPrice);
                cmd.Parameters.AddWithValue("@ActualPrice", bookModel.ActualPrice);
                cmd.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                cmd.Parameters.AddWithValue("@Rating", bookModel.Rating);
                cmd.Parameters.AddWithValue("@RatingCount", bookModel.RatingCount);
                this.sqlConnection.Open();
                var result = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (result != 0)
                {
                    return bookModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
    }
}
