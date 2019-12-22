using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models.ViewModels
{
    /// <summary>
    /// Model that stores Product data with PagingInfo in order to pass a simple object
    /// From Controller to View
    /// </summary>
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        
        
    }
}