using System.Linq;

namespace SportsStore.Models
{
    /// <summary>
    /// Shows the way of getting data from imaginary database
    /// </summary>
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}