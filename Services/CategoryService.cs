using AutoMapper;
using ItiFinalProject.Infrastructure.Repositories;
using ItiFinalProject.Interfaces.Repositories;
using ItiFinalProject.Interfaces.Services;
using ItiFinalProject.Models;
using ItiFinalProject.View_Model.Category;

namespace ItiFinalProject.Services
{
    public class CategoryService : GenericService<Category, CategoryViewModel, CreateCategoryViewModel, UpdateCategoryViewModel>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository, IGenericRepository<Category> repository, IMapper mapper) : base(repository, mapper)
        {
            _categoryRepository = categoryRepository;
        }

        public override async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }
    }
}
