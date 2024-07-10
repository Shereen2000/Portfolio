using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unibooks_backend.Models
{
    public class BookImage
    {
        public int Id { get; set; }
        public string Url {get;set;} = string.Empty;
        public int AdvertId { get; set; }
        public  Advert? Advert{ get; set;}
       
    }
}