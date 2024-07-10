using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unibooks_backend.Models;

namespace unibooks_backend.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(APPUser user);
    }
}