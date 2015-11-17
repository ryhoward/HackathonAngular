using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HackathonWebApi.Membership
{
    public static class UserProvider
    {
        public static User GetUserByLogin(string username, string password)
        {
            User user = null;

            using (var model = new AngularBootcampEntities())
            {
                user = (from users in model.Users
                            where users.Username == username && users.Password == password
                            select new User
                            {
                                UserName = users.Username,
                                Password = users.Password,
                                UserId = users.Id
                            }
                            ).FirstOrDefault();
            }

            return user;
        }

        public static User GetUserByUsername(string username)
        {
            User user = null;

            using (var model = new AngularBootcampEntities())
            {
                user = (from users in model.Users
                        where users.Username == username
                        select new User
                        {
                            UserName = users.Username,
                            Password = users.Password,
                            UserId = users.Id
                        }
                            ).FirstOrDefault();
            }

            return user;
        }
    }
}