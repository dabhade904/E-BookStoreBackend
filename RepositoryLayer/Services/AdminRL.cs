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
    public class AdminRL: IAdminRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }

        public AdminRL(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public AdminRegistrationModel AdminRegistration(AdminRegistrationModel registrationModel)
        {
            {
                try
                {
                    this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
                    SqlCommand cmd = new SqlCommand("SP_Admin", this.sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@AdminName", registrationModel.AdminName);
                    cmd.Parameters.AddWithValue("@AdminEmail", registrationModel.AdminEmail);
                    cmd.Parameters.AddWithValue("@AdminPassword", registrationModel.AdminPassword);
                    cmd.Parameters.AddWithValue("@AdminMobileNumber", registrationModel.AdminMobileNumber);
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
