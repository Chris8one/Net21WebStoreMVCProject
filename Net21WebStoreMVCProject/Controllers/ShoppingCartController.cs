using Microsoft.AspNetCore.Mvc;
using Net21WebStoreMVCProject.Models;
using Net21WebStoreMVCProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net21WebStoreMVCProject.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICandyRepository _candyRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(ICandyRepository candyRepository, ShoppingCart shoppingCart)
        {
            _candyRepository = candyRepository;
            _shoppingCart = shoppingCart;
        }

        // Shows the whole shopping cart
        public ViewResult Index()
        {
            _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                shoppingCart = _shoppingCart,
                shoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        // Redirects to Index, when clicking on a candy you will be redirected to the shopping cart
        public RedirectToActionResult AddToShoppingCart(int candyId)
        {
            var selectedCandy = _candyRepository.GetAllCandy.FirstOrDefault(c => c.CandyId == candyId);

            if (selectedCandy != null)
            {
                _shoppingCart.AddToCart(selectedCandy, 1);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int candyId)
        {
            var selectedCandy = _candyRepository.GetAllCandy.FirstOrDefault(c => c.CandyId == candyId);

            if (selectedCandy != null)
            {
                _shoppingCart.RemoveFromCart(selectedCandy);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult ClearCart()
        {
            _shoppingCart.ClearCart();

            return RedirectToAction("Index");
        }
    }
}
