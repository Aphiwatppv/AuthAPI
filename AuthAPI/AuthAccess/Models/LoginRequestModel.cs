﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAccess.Models
{
    public class LoginRequestModel
    {
        public string UserName { get; set; } 
        public string Password { get; set; }
    }
}
