using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonAppCore.Membership
{
    public class UserRole
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public short RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}