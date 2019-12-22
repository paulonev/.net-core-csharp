using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components
{
    /// <summary>
    /// By General Agreement Components directory store ViewComponents -
    /// reusable codes of application, that can be processed via several
    /// different Controllers
    /// </summary>
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository _repository;

        public NavigationMenuViewComponent(IProductRepository rep)
        {
            _repository = rep;
        }
        
        //Method is invoked from _Layout.cshtml
        public IViewComponentResult Invoke()
        {
            //access to the data of the current query
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}