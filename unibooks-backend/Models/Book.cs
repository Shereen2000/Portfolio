using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace unibooks_backend.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MinLength(13, ErrorMessage = "ISBN must be 13 numbers")]
        [MaxLength(13, ErrorMessage ="ISBN must be 13 numbers")]
        public string ISBN {get;set;} = string.Empty;
        [Required]
        [MaxLength(50, ErrorMessage ="Title cannot exceed 50 characters")]
        public string Title {get; set;} = string.Empty;

        [MaxLength(50, ErrorMessage ="Title cannot exceed 50 characters")]
        public string Publisher {get;set;} = string.Empty;
        
        public string CoverUrl {get;set;}   = string.Empty;
        public List<Advert> Adverts {get;set;} = new List<Advert>();  

    }
}