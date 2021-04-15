using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Controllers.Resources
{
    public class LoginRequestResource
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
