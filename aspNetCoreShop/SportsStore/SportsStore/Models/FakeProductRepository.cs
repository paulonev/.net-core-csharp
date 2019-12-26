using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    /// <summary>
    /// It's a fake storage that simulates the real data storage
    /// </summary>
    public class FakeProductRepository 
        //: IProductRepository
    {
        /// <summary>
        /// returns a collection of queryable objects
        /// </summary>
        public IQueryable<Product> Products => new List<Product>
        {
            new Product {Name = "Football", Price = 25},
            new Product {Name = "Surf board", Price = 179},
            new Product {Name = "Running shoes", Price = 95}
        }.AsQueryable<Product>();
    }
}