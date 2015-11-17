﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonAppCore.Membership
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmailAddress { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}