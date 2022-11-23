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
        public List<CartModel> GetAllBooksinCart(int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:EBookStore"]);
                SqlCommand cmd = new SqlCommand("SP_GetCartBooks", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                List<CartModel> cart = new List<CartModel>();
                cmd.Parameters.AddWithValue("@UserId", userId);
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CartModel model = new CartModel();
                        BookModel bookModel = new BookModel();
                        model.CartId = Convert.ToInt32(reader["CartId"]);
                        bookModel.BookName = reader["BookName"].ToString();
                        bookModel.Author = reader["Author"].ToString();
                        bookModel.Rating = Convert.ToInt32(reader["Rating"]);
                        bookModel.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                        bookModel.ActualPrice = Convert.ToInt32(reader["ActualPrice"]);
                        bookModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                        bookModel.BookDetail = reader["BookDetail"].ToString();
                        bookModel.Quantity = Convert.ToInt32(reader["Quantity"]);
                        bookModel.BookImage = reader["BookImage"].ToString();
                        model.BookId = Convert.ToInt32(reader["BookId"]);
                        model.BooksQty = Convert.ToInt32(reader["BooksQty"]);
                        model.bookModel = bookModel;
                        cart.Add(model);
                    }
                    return cart;
                }
                else
                {
                    return null;
                }
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
    }
}
