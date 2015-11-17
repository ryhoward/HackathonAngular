using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonWebApi.Membership
{
    public class UsersContext
    {
        public List<User> Users { get; set; }
        public List<Role> Roles { get; set; }
        public List<UserRole> UserRoles { get; set; }


        public User GetUser(string userName)
        {
            var user = UserProvider.GetUserByUsername(userName);
            return user;
        }

        public User GetUser(string userName, string password)
        {
            //var user = Users.SingleOrDefault(u => u.UserName == userName && u.Password == password);
            var user = UserProvider.GetUserByLogin(userName, password);
            return user;
        }

        public void AddUserRole(UserRole userRole)
        {
            var roleEntry = UserRoles.SingleOrDefault(r => r.UserId == userRole.UserId);

            //if (roleEntry == null)
            //{
            //    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CloudingoDatabase"].ConnectionString))
            //    {
            //        using (var command = new SqlCommand("dbo.spInsertToolUserRole", connection))
            //        {
            //            command.Parameters.Add("@userId", SqlDbType.Int).Value = userRole.UserId;
            //            command.Parameters.Add("@roleId", SqlDbType.SmallInt).Value = userRole.RoleId;
            //            command.CommandType = CommandType.StoredProcedure;
            //            connection.Open();

            //            command.ExecuteNonQuery();

            //            connection.Close();
            //        }
            //    }
            //}
        }
    }
}