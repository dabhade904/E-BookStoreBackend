using CommonLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
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
                SqlCommand cmd = new SqlCommand("CreateBook_SP", this.sqlConnection)
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
        public BookModel GetBookByBookId(int bookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
                SqlCommand cmd = new SqlCommand("SP_GetBookByBookId", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //adding parameter to store procedure
                cmd.Parameters.AddWithValue("@BookId", bookId);
                this.sqlConnection.Open();
                BookModel book = new BookModel();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                book.BookName = reader["BookName"].ToString();
                book.Author = reader["Author"].ToString();
                book.Rating = Convert.ToInt32(reader["Rating"]);
                book.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                book.ActualPrice = Convert.ToInt32(reader["ActualPrice"]);
                book.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                book.BookDetail = reader["BookDetail"].ToString();
                book.Quantity = Convert.ToInt32(reader["Quantity"]);
                book.BookImage = reader["BookImage"].ToString();
                return book;
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
        public List<GetBookModel> GetAllBooks()
        {
            try
            {
                List<GetBookModel> book = new List<GetBookModel>();
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
                SqlCommand cmd = new SqlCommand("SP_GetAllBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        book.Add(new GetBookModel
                        {
                            ID = Convert.ToInt32(reader["Id"]),
                            BookName = reader["BookName"].ToString(),
                            Author = reader["Author"].ToString(),
                            Rating = Convert.ToInt32(reader["Rating"]),
                            RatingCount = Convert.ToInt32(reader["RatingCount"]),
                            DiscountPrice = Convert.ToInt64(reader["DiscountPrice"]),
                            ActualPrice = Convert.ToInt64(reader["ActualPrice"]),
                            BookDetail = reader["BookDetail"].ToString(),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            BookImage = reader["BookImage"].ToString()
                        });
                    }
                    this.sqlConnection.Close();
                    return book;
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
        public bool DeleteBook(int bookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
                SqlCommand cmd = new SqlCommand("SP_DeleteBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@BookId", bookId);
                this.sqlConnection.Open();
                var result = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;

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
        public bool UpdateBook(int BookId, BookModel updateBook)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
                SqlCommand cmd = new SqlCommand("SP_UpdateBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //adding parameter to store procedure
                cmd.Parameters.AddWithValue("@BookId", BookId);
                cmd.Parameters.AddWithValue("@BookName", updateBook.BookName);
                cmd.Parameters.AddWithValue("@Author", updateBook.Author);
                cmd.Parameters.AddWithValue("@Rating", updateBook.Rating);
                cmd.Parameters.AddWithValue("@RatingCount", updateBook.RatingCount);
                cmd.Parameters.AddWithValue("@DiscountPrice", updateBook.DiscountPrice);
                cmd.Parameters.AddWithValue("@ActualPrice", updateBook.ActualPrice);
                cmd.Parameters.AddWithValue("@BookDetail", updateBook.BookDetail);
                cmd.Parameters.AddWithValue("@Quantity", updateBook.Quantity);
                cmd.Parameters.AddWithValue("@BookImage", updateBook.BookImage);

                sqlConnection.Open();
                cmd.ExecuteScalar();
                return true;
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
