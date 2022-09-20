using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net21WebStoreMVCProject.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public List<Candy> Candies { get; set; }
    }
}
