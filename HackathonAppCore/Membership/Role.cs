using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonAppCore.Membership
{
    public class Role
    {
        public short RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
}