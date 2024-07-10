using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Util.Internal;
using unibooks_backend.Models;

namespace unibooks_backend.Dtos.Book
{
    public class CreateBookRequestDto
    {
      
        public string ISBN {get;set;} = string.Empty;
        public string Title {get; set;} = string.Empty;
        public string Publisher {get;set;} = string.Empty;
       
    }
}