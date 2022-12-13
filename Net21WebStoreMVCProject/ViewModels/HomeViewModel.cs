using Net21WebStoreMVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net21WebStoreMVCProject.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Candy> CandyOnSale { get; set; }
    }
}
