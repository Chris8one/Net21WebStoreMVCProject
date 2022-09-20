using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net21WebStoreMVCProject.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> GetAllCategories => new List<Category>
        {
            new Category{ CategoryId = 1, CategoryName = "Hard Candy", CategoryDescription = "Delicious hard candy" },
            new Category{ CategoryId = 2, CategoryName = "Chocolate Candy", CategoryDescription = "Delicious chocolate candy" },
            new Category{ CategoryId = 3, CategoryName = "Fruit Candy", CategoryDescription = "Sweet and Sour fruit candy" },
        };
    }
}
