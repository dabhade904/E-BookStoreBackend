using CommonLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public string AdminLogin(AdminLoginModel model)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
            using (sqlConnection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("Login_Admin", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    //  var pass = DecryptPassword(loginModel.Password);
                    command.Parameters.AddWithValue("@AdminEmail", model.AdminEmail);
                    command.Parameters.AddWithValue("@AdminPassword",model.AdminPassword);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string query = "SELECT ID FROM AdminData WHERE AdminEmail = '" + result + "'";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection);
                        var Id = cmd.ExecuteScalar();
                        var token = GenerateSecurityToken(model.AdminEmail, Id.ToString());
                        return token;

                    }
                    else
                    {
                        return null;
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
        }
        public string GenerateSecurityToken(string email, string userID)
        {
            try
            {
                var loginTokenHandler = new JwtSecurityTokenHandler();
                var loginTokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.Configuration[("Jwt:key")]));
                var loginTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                       new Claim(ClaimTypes.Email, email),
                        new Claim("ID", userID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(loginTokenKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = loginTokenHandler.CreateToken(loginTokenDescriptor);
                return loginTokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
