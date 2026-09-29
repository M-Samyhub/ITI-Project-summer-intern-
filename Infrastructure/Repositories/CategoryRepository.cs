using ItiFinalProject.Infrastructure.Data;
using ItiFinalProject.Interfaces.Repositories;
using ItiFinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ItiFinalProject.Infrastructure.Repositories
{
    public class CategoryRepository : GenericReopsitory<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
            .Include(c => c.Products)
            .ToListAsync();
        }
    }
}
