using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net21WebStoreMVCProject.Models
{
    public class CandyRepository : ICandyRepository
    {
        private readonly AppDbContext _appDbContext;

        public CandyRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //public IEnumerable<Candy> GetAllCandy => new List<Candy>
        //{
        //    new Candy { CandyId = 1, Name = "Assorted Hard Candy", Price = 4.95m, Description = "Lorem Ipsum dada lita popo dalla rela ko.",
        //        Category = _categoryRepository.GetAllCategories.ToList()[0], ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/1/13/HardCandy.jpg",
        //        ImageThumbnailUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/HardCandy.jpg/220px-HardCandy.jpg",
        //        IsInStock = true, IsOnSale = false
        //    },

        //    new Candy { CandyId = 2, Name = "Assorted Chocolate Candy", Price = 6.95m, Description = "Lorem Ipsum dada lita popo dalla rela ko.",
        //        Category = _categoryRepository.GetAllCategories.ToList()[1], ImageUrl = "https://upload.wikimedia.org/wikipedia/en/3/3c/Candy-Peanut-MMs-Wrapper-Small.jpg",
        //        ImageThumbnailUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a8/M%26m2.jpg/220px-M%26m2.jpg",
        //        IsInStock = true, IsOnSale = true
        //    },

        //    new Candy { CandyId = 3, Name = "Assorted Fruit Candy", Price = 2.95m, Description = "Lorem Ipsum dada lita popo dalla rela ko.",
        //        Category = _categoryRepository.GetAllCategories.ToList()[2], ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2b/Betty_Crocker_Fruit_Gushers_pieces.jpg/1024px-Betty_Crocker_Fruit_Gushers_pieces.jpg",
        //        ImageThumbnailUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2b/Betty_Crocker_Fruit_Gushers_pieces.jpg/220px-Betty_Crocker_Fruit_Gushers_pieces.jpg",
        //        IsInStock = true, IsOnSale = true
        //    },
        //};

        public IEnumerable<Candy> GetCandyOnSale
        {
            get
            {
                return _appDbContext.Candies.Include(c => c.Category).Where(p => p.IsOnSale);
            }
        }

        public IEnumerable<Candy> GetAllCandy
        {
            get {
                    return _appDbContext.Candies.Include(c => c.Category);
                } 
        }

        public Candy GetCandyById(int candyId)
        {
            return _appDbContext.Candies.FirstOrDefault(c => c.CandyId == candyId);
        }
    }
}
