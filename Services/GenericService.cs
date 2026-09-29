using AutoMapper;
using ItiFinalProject.Interfaces.Repositories;
using ItiFinalProject.Interfaces.Services;

namespace ItiFinalProject.Services
{
    public class GenericService<TEntity, TViewModel, TCreateViewModel, TUpdateViewModel>
        : IGenericService<TViewModel, TCreateViewModel, TUpdateViewModel> where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public virtual async Task<IEnumerable<TViewModel>> GetAllAsync()
        {
            var entitys = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TViewModel>>(entitys);
        }

        public virtual async Task<TViewModel?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return default;

            return _mapper.Map<TViewModel>(entity);
        }
        public virtual async Task<TUpdateViewModel?> GetForEditByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return default;

            return _mapper.Map<TUpdateViewModel>(entity);
        }
        public virtual async Task CreateAsync(TCreateViewModel viewModel)
        {
            var entity = _mapper.Map<TEntity>(viewModel);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(int id, TUpdateViewModel updateViewModel)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return;

            _mapper.Map(updateViewModel, entity);

            _repository.Update(entity);
            await _repository.SaveChangesAsync();

        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return;

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
        }
    }
}
