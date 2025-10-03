﻿
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? street { get; set; }
        public string? city { get; set; }
        public string? CodeResetPassword { get; set; }
        public DateTime? PasswordResetCodeExpierty { get; set; }
    }
}
