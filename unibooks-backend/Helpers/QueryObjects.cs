using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpers
{
    public class BookQueryObject
    {
        public string? SortBy {get;set;} = null;
        public string? Title {get;set;} = null;
        public string? Publisher {get;set;} = null;
        public bool IsDescending {get;set;} = false;
        
    }

     public class AdvertQueryObject
    {
        public string? SortBy {get;set;} = null;
        public string? Title {get;set;} = null;
        public string? Publisher {get;set;} = null;
        public bool IsDescending {get;set;} = false;
        
    }
}