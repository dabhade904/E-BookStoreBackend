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
    public class WishlistRL : IWishlistRL
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

        public List<WishlistModel> GetAllBooksinWishList(int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
                SqlCommand cmd = new SqlCommand("SP_GetAllWishListBooks", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                List<WishlistModel> wishList = new List<WishlistModel>();
                cmd.Parameters.AddWithValue("@UserId", userId);
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        WishlistModel model = new WishlistModel();
                        BookModel bookModel = new BookModel();
                        model.userId = Convert.ToInt32(reader["UserId"]);
                        model.wishListId = Convert.ToInt32(reader["wishListId"]);
                        bookModel.BookName = reader["BookName"].ToString();
                        bookModel.Author = reader["Author"].ToString();
                        bookModel.Rating = Convert.ToInt32(reader["Rating"]);
                        bookModel.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                        bookModel.ActualPrice = Convert.ToInt32(reader["ActualPrice"]);
                        bookModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                        bookModel.BookDetail = reader["BookDetail"].ToString();
                        bookModel.Quantity = Convert.ToInt32(reader["Quantity"]);
                        bookModel.BookImage = reader["BookImage"].ToString();
                        model.bookId = Convert.ToInt32(reader["BookId"]);
                        model.bookModel = bookModel;
                        wishList.Add(model);
                    }
                    return wishList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public string DeleteFromWishList(int wishListId)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
            using (sqlConnection)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_DeleteWishlist", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@WishListId", wishListId);

                    sqlConnection.Open();
                    var result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();

                    if (result != 0)
                    {
                        return "Item Removed from WishList Successfully";
                    }
                    else
                    {
                        return "Failed to Remove item from WishList";
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
