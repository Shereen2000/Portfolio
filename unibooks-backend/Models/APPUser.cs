using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace unibooks_backend.Models
{
    public class APPUser: IdentityUser
    {
        public List<BookMark> BookMarks { get; set; } = new List<BookMark>();
    }
}