using ItiFinalProject.Models;

namespace ItiFinalProject.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithCategoryAsync();
        Task<Product> GetByIdWithCategory(int id);
    }
}
