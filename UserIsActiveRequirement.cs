using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI
{
    public class UserIsActiveRequirement: IAuthorizationRequirement
    {
        public bool IsActive { get; set; }
        public UserIsActiveRequirement(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
