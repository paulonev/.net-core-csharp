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
                // data visualized by Action Method where user is redirected, i.e Index
                // a Session state facility that hosts data until it will be executed somewhere in code
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else return View(product);
        }

        /// <summary>
        /// Creates new product and save it to database
        /// </summary>
        /// <returns></returns>
        public ViewResult Create() => View("Edit", new Product());

        
        /// <summary>
        /// Invokes DeleteProduct() of data storage which returns deleted Product
        /// </summary>
        /// <param name="productId">id of product to delete</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }

            return RedirectToAction("Index", "Admin");
        }
    }
}