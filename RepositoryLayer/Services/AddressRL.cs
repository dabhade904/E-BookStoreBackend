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
    public class AddressRL : IAddressRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration configuration;
        public AddressRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string AddAddress(int UserId, AddressModel addressModel)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:EBookStore"]);
                SqlCommand cmd = new SqlCommand("SP_AddAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Address", addressModel.Address);
                cmd.Parameters.AddWithValue("@City", addressModel.City);
                cmd.Parameters.AddWithValue("@State", addressModel.State);
                cmd.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                this.sqlConnection.Open();
                cmd.ExecuteNonQuery();
                return " Address Added Successfully";
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
        public bool UpdateAddress(int addressId, AddressModel addressModel)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:EBookStore"]);
                SqlCommand cmd = new SqlCommand("SP_UpdateAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AddressId", addressId);
                cmd.Parameters.AddWithValue("@Address", addressModel.Address);
                cmd.Parameters.AddWithValue("@City", addressModel.City);
                cmd.Parameters.AddWithValue("@State", addressModel.State);
                cmd.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                this.sqlConnection.Open();
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
