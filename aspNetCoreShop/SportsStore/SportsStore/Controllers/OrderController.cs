using System.Linq;
using Microsoft.AspNetCore.Authorization;
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

        //ADDITION to without-catalogManagement BLOCK
        [Authorize]
        public ViewResult List() => View(repository.Orders.Where(o => !o.Shipped));

        /// <summary>
        /// This method receives POST, which indicates Order ID
        /// Authorize attr makes Action Methods to be executed only if request is authorized 
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderID)
        {
            Order order = repository.Orders
                .FirstOrDefault(ord => ord.OrderID == orderID);
            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }

            return RedirectToAction(nameof(List));
        }
    }
}