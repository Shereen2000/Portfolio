using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unibooks_backend.Dtos.Advert
{
    public class CreateAdvertRequestDto
    {
      public decimal Price { get; set; }
      public string Condition {get;set;} = string.Empty;
      public int BookId {get;set;}
      
    }
}