using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HackathonWebApi.Membership
{
    public class CustomRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            var usersContext = new UsersContext();
            var user = usersContext.Users.SingleOrDefault(u => u.UserName == username);
            if (user == null)
                return false;
            return user.UserRoles != null && user.UserRoles.Select(
                 u => u.Role).Any(r => r.RoleName == roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            var usersContext = new UsersContext();
            //var user = usersContext.Users.SingleOrDefault(u => u.UserName == username);
            var user = usersContext.GetUser(username);
            if (user == null)
                return new string[] { };
            return user.UserRoles == null
                ? new string[] { }
                : GetUserRoles(user.UserId).Select(u => u.Role).Select(u => u.RoleName).ToArray();
            //user.UserRoles.Select(u => u.Role).Select(u => u.RoleName).ToArray();
        }

        private IEnumerable<UserRole> GetUserRoles(int userId)
        {
            List<UserRole> userRoles = null;


            //using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CloudingoDatabase"].ConnectionString))
            //{
            //    using (var command = new SqlCommand("dbo.spGetUserRolesById", connection))
            //    {
            //        command.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
            //        command.CommandType = CommandType.StoredProcedure;

            //        connection.Open();

            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            userRoles = new List<UserRole>();
            //            while (reader.Read())
            //            {
            //                userRoles.Add(new UserRole
            //                {
            //                    UserId = userId,
            //                    RoleId = (short)Convert.ToUInt16(reader["RoleId"]),
            //                    UserRoleId = Convert.ToInt32(reader["UserRoleId"]),
            //                    Role = new Role
            //                    {
            //                        RoleName = Convert.ToString(reader["RoleName"]),
            //                        RoleDescription = Convert.ToString(reader["RoleDescription"])
            //                    }
            //                });
            //            }
            //        }
            //    }
            //}
            return userRoles;
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            var usersContext = new UsersContext();
            return usersContext.Roles.Select(r => r.RoleName).ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}