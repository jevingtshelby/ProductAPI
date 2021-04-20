using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI
{
    public interface IUserIsActive
    {
        bool Get(int isActive);
    }
}
