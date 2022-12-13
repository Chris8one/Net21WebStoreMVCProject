using Net21WebStoreMVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net21WebStoreMVCProject.ViewModels
{
    public class CandyProductViewModel
    {
        public Candy Candy { get; set; }
        public IEnumerable<Category> Category { get; set; }
    }
}
