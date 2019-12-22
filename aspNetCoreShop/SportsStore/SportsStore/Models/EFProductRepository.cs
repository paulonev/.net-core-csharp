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
    }
}