using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        
        public OrderController(IOrderRepository rep, Cart cartService)
        {
            repository = rep;
            cart = cartService;
        }
        
        public ViewResult Checkout() => View(new Order());

        /// <summary>
        /// Handle POST request from the Checkout View
        /// </summary>
        /// <param name="order">Order Model Binding(Привязка модели) from Checkout Razor View</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!cart.Lines.Any())
            {
                ModelState.AddModelError("","Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else return View(order);
        }

        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}