using Microsoft.AspNetCore.Mvc;
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
    }
}