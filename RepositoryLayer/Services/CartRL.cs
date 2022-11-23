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
    public class CartRL : ICartRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration configuration;
        public CartRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AddBookToCart(CartModel cartModel, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:EBookStore"]);
                SqlCommand cmd = new SqlCommand("SP_AddCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //adding parameter to store procedure
                cmd.Parameters.AddWithValue("@BooksQty", cartModel.BooksQty);
                cmd.Parameters.AddWithValue("@BookId", cartModel.BookId);
                cmd.Parameters.AddWithValue("@UserId", userId);

                this.sqlConnection.Open();
                cmd.ExecuteNonQuery();
                return "book added in cart successfully";
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

    }
}
