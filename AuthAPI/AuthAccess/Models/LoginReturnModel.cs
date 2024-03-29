using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAccess.Models
{
    public class LoginReturnModel
    {
        public int UserID { get; set; }
        public string Salt { get; set; }
        public string PasswordHash { get; set; }
    }
}
