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
        private readonly IConfiguration configuration;
        SqlConnection sqlConnection;
        public WishlistRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string AddToWishList(int bookId, int userId)
        {
            this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:EBookStore"]);
            using (sqlConnection)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_AddToWishList", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    sqlConnection.Open();
                    var result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();

                    if (result > 0)
                    {
                        return "Added to WishList Successfully";
                    }
                    else
                    {
                        return "Failed to Add to WishList";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }
        public List<WishlistModel> GetAllWishList(int userId)
        {
            this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:EBookStore"]);
            using (sqlConnection)
            {
                try
                {
                    List<WishlistModel> wishListResponse = new List<WishlistModel>();
                    SqlCommand cmd = new SqlCommand("SP_GetAllWishList", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);

                    sqlConnection.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            WishlistModel wishList = new WishlistModel
                            {
                                BookId = Convert.ToInt32(rdr["BookId"]),
                                UserId = Convert.ToInt32(rdr["UserId"]),
                                WishListId = Convert.ToInt32(rdr["WishListId"]),
                                BookName = Convert.ToString(rdr["BookName"]),
                                Author = Convert.ToString(rdr["Author"]),
                                BookImage = Convert.ToString(rdr["BookImage"]),
                                DiscountPrice = Convert.ToDouble(rdr["DiscountPrice"]),
                                ActualPrice = Convert.ToDouble(rdr["ActualPrice"])
                            };
                            wishListResponse.Add(wishList);
                        }
                        sqlConnection.Close();
                        return wishListResponse;
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
        public string RemoveFromWishList(int wishListId)
        {
            this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:EBookStore"]);
            using (sqlConnection)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_RemoveFromWishList", sqlConnection);
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
