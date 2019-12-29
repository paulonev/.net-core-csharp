using System.Linq;

namespace SportsStore.Models
{
    /// <summary>
    /// Shows the way of getting data from imaginary database
    /// </summary>
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        
        /// <summary>
        /// The need to edit data in database entity; comes from AdminController - Edit
        /// </summary>
        /// <returns></returns>
        void SaveProduct(Product product);

        Product DeleteProduct(int productID);

    }
}