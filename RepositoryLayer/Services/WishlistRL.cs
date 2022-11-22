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
    public class WishlistRL:IWishlistRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }

        public WishlistRL(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public string AddBookinWishList(WishlistModel wishListModel, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
                SqlCommand cmd = new SqlCommand("SP_CreateWishList", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@BookId", wishListModel.bookId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                return "book is added in WishList successfully";
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
