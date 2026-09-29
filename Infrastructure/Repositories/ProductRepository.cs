using ItiFinalProject.Infrastructure.Data;
using ItiFinalProject.Interfaces.Repositories;
using ItiFinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ItiFinalProject.Infrastructure.Repositories
{
    public class ProductRepository : GenericReopsitory<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
        {
            return await _context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product> GetByIdWithCategory(int id)
        {
            return await _context.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
