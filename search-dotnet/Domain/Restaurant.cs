using System;
using System.Collections.Generic;

namespace JustEat.Search.Domain
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }      
        public Rating Rating { get; set; }
        public string Description { get; set; } 
        public List<Cuisine> Cuisines { get; set; }
    }
}
