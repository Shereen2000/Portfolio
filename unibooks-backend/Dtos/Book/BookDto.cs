using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unibooks_backend.Dtos.Advert;
using unibooks_backend.Models;

namespace unibooks_backend.Dtos.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string ISBN {get;set;} = string.Empty;
        public string Title {get; set;} = string.Empty;
        public string Publisher {get;set;} = string.Empty;
        public List<AdvertDto> Adverts {get;set;}
    }
}