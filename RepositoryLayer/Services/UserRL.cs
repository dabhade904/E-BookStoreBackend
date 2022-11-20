using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public string UserLogin(LoginModel loginModel)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
            using (sqlConnection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("Login_User", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();

                    command.Parameters.AddWithValue("@EmailId", loginModel.Email);
                    command.Parameters.AddWithValue("@Password", loginModel.Password);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string query = "SELECT ID FROM Users WHERE EmailId = '" + result + "'";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection);
                        var Id = cmd.ExecuteScalar();
                        var token = GenerateSecurityToken(loginModel.Email, Id.ToString());
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