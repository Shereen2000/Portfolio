using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unibooks_backend.Dtos.AdvertPicture;
using unibooks_backend.Models;

namespace unibooks_backend.Dtos.Advert
{
    public class AdvertDto
    {
      public int Id { get; set; }
      public decimal Price { get; set; }
      public DateTime CreatedOn {get;set;} = DateTime.Now;
      public string Condition {get;set;} = string.Empty;
      public List<AdvertPictureDto> Advertimages = new List<AdvertPictureDto>();
      public int BookId {get;set;}
      public string Title {get;set;} = string.Empty;
      public string ISBN {get;set;} = string.Empty;
      public string CoverUrl {get;set;} = string.Empty;



    }
}