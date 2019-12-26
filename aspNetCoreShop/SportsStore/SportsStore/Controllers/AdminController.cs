using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    
    /// <summary>
    /// Controller for CRUD(create, read, update, delete) actions over elements of Product collection
    /// Would be able to work only in Admin Mode(no plain user access) 
    /// </summary>
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository rep)
        {
            repository = rep;
        }

        public ViewResult Index() => View(repository.Products);

        public ViewResult Edit(int productId) =>
            View(repository.Products.FirstOrDefault(p => p.ProductID == productId));

        
        //Method that accepts POST from Edit View
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            //check if validation of input view was successful
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                //data visualized by action method where user is redirected, i.e Index
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else return View(product);
        }
    }
}