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
    public class UserRL : IUserRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }

        public UserRL(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public UserRegistrationModel UserRegistration(UserRegistrationModel registrationModel)
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
        public string UserLogin(UserLoginModel loginModel)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
            using (sqlConnection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("Login_User", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                  //  var pass = DecryptPassword(loginModel.Password);
                    command.Parameters.AddWithValue("@EmailId", loginModel.Email);
                    command.Parameters.AddWithValue("@Password",loginModel.Password);

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
                        new Claim(ClaimTypes.Role, "User"),
                        new Claim(ClaimTypes.Email, email),
                       // new Claim("ID", userID.ToString()),
                        new Claim("userID",userID.ToString())
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
        public string ForgetPassword(string Email)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();
                    string query = "SELECT EmailId FROM Users WHERE EmailId = '" + Email + "'";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    var email = cmd.ExecuteScalar();
                    string query1 = "SELECT ID FROM Users WHERE EmailId = '" + Email + "'";
                    SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
                    var id = sqlCommand.ExecuteScalar();
                    if (email != null)
                    {
                        var token = GenerateSecurityToken(email.ToString(), id.ToString());
                        MSMQModel msmqModel = new MSMQModel();
                        msmqModel.sendData2Queue(token);
                        return token;
                    }
                    else
                        return null;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public bool ResetPassword(string resetPassword, string confirmPassword, string EmailId)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:EBookStore"]);
            using (sqlConnection)
            {
                try
                {
                    if (resetPassword.Equals(confirmPassword))
                    {
                        SqlCommand command = new SqlCommand("SP_ResetPassword", sqlConnection);
                        command.CommandType = CommandType.StoredProcedure;
                        sqlConnection.Open();
                        command.Parameters.AddWithValue("@EmailId", EmailId);
                        command.Parameters.AddWithValue("@Password",resetPassword);
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                        return false;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public string EncryptPassword(string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(password))
                {
                    byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
                    string encryptPassword = Convert.ToBase64String(storePassword);
                    return encryptPassword;
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
        }
        public string DecryptPassword(string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(password))
                {
                    byte[] encryptedPassword = Convert.FromBase64String(password);
                    string decryptPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
                    return decryptPassword;
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
        }
    }
}