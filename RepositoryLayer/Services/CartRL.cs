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
    public class CartRL : ICartRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration configuration;
        public CartRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AddBookToCart(CartModel cartModel, int userId, int bookId)
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
                cmd.Parameters.AddWithValue("@BookId", bookId);
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
        public string DeleteCart(int cartId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:EBookStore"]);
                SqlCommand cmd = new SqlCommand("SP_DeleteCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CartId", cartId);
                sqlConnection.Open();
                cmd.ExecuteScalar();
                return "Book Deleted in Cart Successfully";
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public bool UpdateCart(int cartId, int booksQty)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:EBookStore"]);
                SqlCommand cmd = new SqlCommand("SP_UpdateCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CartId", cartId);
                cmd.Parameters.AddWithValue("@BooksQty", booksQty);
                sqlConnection.Open();
                cmd.ExecuteScalar();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public List<CartResponce> GetAllBooksinCart(int userId)
        {
            this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:EBookStore"]);
            using (sqlConnection)
            {
                try
                {
                    List<CartResponce> cartResponses = new List<CartResponce>();
                    SqlCommand cmd = new SqlCommand("spGetAllCart", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);

                    sqlConnection.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            CartResponce cart = new CartResponce();
                            cart.BookId = Convert.ToInt32(rdr["BookId"]);
                            cart.UserId = Convert.ToInt32(rdr["UserId"]);
                            cart.CartId = Convert.ToInt32(rdr["CartId"]);
                            cart.BookName = Convert.ToString(rdr["BookName"]);
                            cart.Author = Convert.ToString(rdr["Author"]);
                            cart.BookImage = Convert.ToString(rdr["BookImage"]);
                            cart.DiscountPrice = Convert.ToDouble(rdr["DiscountPrice"]);
                            cart.ActualPrice = Convert.ToDouble(rdr["ActualPrice"]);
                            cart.CartsQty = Convert.ToInt32(rdr["BooksQty"]);
                            cart.Stock = Convert.ToInt32(rdr["Quantity"]);
                            cartResponses.Add(cart);
                        }

                        sqlConnection.Close();
                        return cartResponses;
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
