using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace unibooks_backend.Models
{
    public class Advert
    {
      public int Id { get; set; }
     [Column(TypeName = "decimal(18,2)")]
      public decimal Price { get; set; }
      public DateTime CreatedOn {get;set;} = DateTime.Now;
      public string Condition {get;set;} = string.Empty;
      public int BookId {get;set;}
      public Book? Book{get;set;}
      public List<BookImage> BookImages{get;set;} = new List<BookImage>();
      public List<BookMark> BookMarks{get;set;}=new List<BookMark>();
      
    }
}