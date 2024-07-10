using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unibooks_backend.Dtos.Advert
{
    public class AdvertDto
    {
      public int Id { get; set; }
      public decimal Price { get; set; }
      public DateTime CreatedOn {get;set;} = DateTime.Now;
      public string Condition {get;set;} = string.Empty;
      public int BookId {get;set;}

    }
}