using Microsoft.AspNetCore.Mvc;
using Net21WebStoreMVCProject.Models;
using Net21WebStoreMVCProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net21WebStoreMVCProject.Controllers
{
    public class CandyHomeController : Controller
    {
        private readonly ICandyRepository _candyRepository;

        public CandyHomeController(ICandyRepository candyRepo)
        {
            _candyRepository = candyRepo;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                CandyOnSale = _candyRepository.GetCandyOnSale
            };

            return View(homeViewModel);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
