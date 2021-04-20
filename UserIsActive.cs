using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI
{
    public class UserIsActive : IUserIsActive
    {
        public bool Get(int isActive)
        {
            if (isActive == 1)
                return true;
            return false;
           
        }
    }
}
