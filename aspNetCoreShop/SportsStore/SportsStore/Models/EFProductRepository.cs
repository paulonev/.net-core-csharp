using System.Linq;

namespace SportsStore.Models
{
    
    /// <summary>
    /// Storage(repository) class
    /// </summary>
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        public EFProductRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        
        public IQueryable<Product> Products => _context.Products;

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                //
                // This approach of changing Product's context is quite trivial but one must know
                // that EntityFramework services track each own object added to the entity
                // So first of all it's necessarily to find object in context of DB, cause it knows
                // nothing literally about Product product passed here as argument
                //  
                Product dbEntry = _context.Products
                    .FirstOrDefault(pr => pr.ProductID == product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.ChangeData(product.Name, product.Description,
                        product.Price, product.Category);
                }
            }

            _context.SaveChanges();
        }
    }
}