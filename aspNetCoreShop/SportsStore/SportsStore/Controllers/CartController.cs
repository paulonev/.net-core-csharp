using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private Cart cart; 
        
        public CartController(IProductRepository rep, Cart cartService)
        {
            repository = rep;
            //service AddScoped<Cart> allows to create a SessionCart whenever it's requested 
            cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(pr => pr.ProductID == productId);
            if (product != null)
            {
                cart.AddItem(product,1);
            }

            return RedirectToAction("Index", new {returnUrl});
        }
        
        /// <summary>
        /// Implement Principle "Model association"
        /// When elements from input of HTML <form> are passed here as arguments
        /// when asp-controller and asp-action are specified for this action method
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(pr => pr.ProductID == productId);
            if (product != null)
            {
                cart.RemoveItem(product);
            }

            return RedirectToAction("Index", new {returnUrl});
        }

//        private Cart GetCart()
//        {
//            //Session state util
//            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
//            return cart;
//        }
//
//        private void SaveCart(Cart cart)
//        {
//            //Session state util
//            HttpContext.Session.SetJson("Cart", cart);
//        }
    }
}