using Net21WebStoreMVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net21WebStoreMVCProject.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart shoppingCart { get; set; }
        public decimal shoppingCartTotal { get; set; }
    }
}
