using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HackathonApplication.Membership
{
    public static class UserProvider
    {
        public static User GetUserByLogin(string username, string password)
        {
            User user = null;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CloudingoDatabase"].ConnectionString))
            {
                using (var command = new SqlCommand("dbo.spGetToolUser", connection))
                {
                    command.Parameters.Add("@userName", SqlDbType.NVarChar).Value = username;
                    command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new User
                            {
                                UserName = username,
                                Password = password,
                                UserId = Convert.ToInt32(reader["UserId"])
                            };
                        }
                    }

                    connection.Close();
                }
            }
            return user;
        }

        public static User GetUserByUsername(string username)
        {
            User user = null;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CloudingoDatabase"].ConnectionString))
            {
                using (var command = new SqlCommand("dbo.spGetToolUser", connection))
                {
                    command.Parameters.Add("@userName", SqlDbType.NVarChar).Value = username;
                    command.Parameters.Add("@password", SqlDbType.NVarChar).Value = "";
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new User
                            {
                                UserName = username,
                                Password = Convert.ToString(reader["Password"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                UserRoles = new List<UserRole>()
                            };
                        }
                    }

                    connection.Close();
                }
            }
            return user;
        }
    }
}