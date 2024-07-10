using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unibooks_backend.Models
{
    public class BookMark
    {
        public string AppUserId { get; set; }   
        public int AdvertId {get;set;}
        public APPUser AppUser { get; set; }
        public Advert Advert { get; set; }
    }
}