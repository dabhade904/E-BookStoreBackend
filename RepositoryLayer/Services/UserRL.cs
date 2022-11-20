using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL :IUserRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }

        public UserRL(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public RegistrationModel UserRegistration(RegistrationModel registrationModel)
        {
            {
                try
                {
                    this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
                    SqlCommand cmd = new SqlCommand("dbo.Add_User", this.sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@FullName", registrationModel.FullName);
                    cmd.Parameters.AddWithValue("@EmailId", registrationModel.Email);
                    cmd.Parameters.AddWithValue("@Password", registrationModel.Password);
                    cmd.Parameters.AddWithValue("@MobileNumber", registrationModel.MobileNumber);
                    this.sqlConnection.Open();
                    var result = cmd.ExecuteNonQuery();
                    this.sqlConnection.Close();
                    if (result != 0)
                    {
                        return registrationModel;
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
}
