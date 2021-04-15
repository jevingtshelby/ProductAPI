using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Controllers.Resources
{
    public class LoginResponseResource
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
