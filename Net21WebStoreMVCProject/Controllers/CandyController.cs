using Microsoft.AspNetCore.Mvc;
using Net21WebStoreMVCProject.Models;
using Net21WebStoreMVCProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net21WebStoreMVCProject.Controllers
{
    public class CandyController : Controller
    {
        private readonly ICandyRepository _candyRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CandyController(ICandyRepository candyRepo, ICategoryRepository categoryRepo)
        {
            _candyRepository = candyRepo;
            _categoryRepository = categoryRepo;
        }

        public IActionResult List()
        {
            //ViewBag.CurrentCategory = "Best Seller";
            //return View(_candyRepository.GetAllCandy);
            var candyListViewModel = new CandyListViewModel();
            candyListViewModel.Candies = _candyRepository.GetAllCandy;
            candyListViewModel.CurrentCategory = "Best Seller";

            return View(candyListViewModel);
        }
    }
}
