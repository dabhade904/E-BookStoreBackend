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
    public class OrderRL : IOrderRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }
        public OrderRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public string AddOrder(OrderModel order)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "SP_AddingOrders";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", order.UserId);
                    sqlCommand.Parameters.AddWithValue("@AddressId", order.AddressId);
                    sqlCommand.Parameters.AddWithValue("@BookId", order.BookId);
                    sqlCommand.Parameters.AddWithValue("@BookQuantity", order.BookQuantity);

                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 2)
                    {
                        return "BookId not exists";
                    }
                    else if (result == 1)
                    {
                        return "UserId not exists";
                    }
                    else
                    {
                        return "Ordered successfully";
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public List<GetOrderDetailsModel> AllOrderDetails(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "SP_GetAllOrders";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    List<GetOrderDetailsModel> order = new List<GetOrderDetailsModel>();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            GetOrderDetailsModel orderModel = new GetOrderDetailsModel();
                            BookModel getbookModel = new BookModel();
                            getbookModel.ID = Convert.ToInt32(sqlData["ID"]);
                            getbookModel.BookName = sqlData["BookName"].ToString();
                            getbookModel.Author = sqlData["Author"].ToString();
                            getbookModel.DiscountPrice = Convert.ToInt32(sqlData["DiscountPrice"]);
                            getbookModel.ActualPrice = Convert.ToInt32(sqlData["ActualPrice"]);
                            getbookModel.BookDetail = sqlData["BookDetail"].ToString();
                            getbookModel.BookImage = sqlData["BookImage"].ToString();
                            orderModel.OrderId = Convert.ToInt32(sqlData["OrdersId"]);
                            orderModel.OrderDate = sqlData["OrderDate"].ToString();
                            orderModel.model = getbookModel;
                            order.Add(orderModel);
                        }
                        return order;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public string DeleteOrder(int ordersId, int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
            using (sqlConnection)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_RemoveFromOrder", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@OrdersId", ordersId);
                    cmd.Parameters.AddWithValue("@UserId", userId);


                    sqlConnection.Open();
                    var result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();

                    if (result != 0)
                    {
                        return "Order Deleted Successfully";
                    }
                    else
                    {
                        return "Failed to Delete the Order";
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