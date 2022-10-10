using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK_Project.Models.Requests
{
    public class LoginRequest
    {
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}
