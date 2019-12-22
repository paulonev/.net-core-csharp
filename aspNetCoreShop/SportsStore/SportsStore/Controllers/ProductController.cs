using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        public int PageSize = 4;

        public ProductController(IProductRepository rep)
        {
            _repository = rep;
        }

        //solve the issue of paging
        //display PageSize amount of products per page
        public ViewResult List(string category, int productPage = 1) =>
            View(new ProductsListViewModel
            {
                Products = _repository.Products
                    .Where(p=>category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
//                    TotalItems = _repository.Products.Count()
                    TotalItems = category == null ?
                        _repository.Products.Count() :
                        _repository.Products.Where(e => 
                            e.Category == category).Count()
                },
                CurrentCategory = category
            });

        ///
        /// We have a controller with Action Method (List), which MVC will apply
        /// whenever a standard URL for WebApp is requested. MVC generates an instance
        /// of FakeRepository class(stores static data) and will use it in creating
        /// new Controller instance, processing a query. Fake storage will give the
        /// controller a bunch of fake static data, which will delegate it to Razor
        /// Views. While generation HTML-response MVC combines data from view selected
        /// by Action Methods with content of Shared Layout producing a finite response
        /// which a browser could read and show. Fuuh.
        /// 
    }
}