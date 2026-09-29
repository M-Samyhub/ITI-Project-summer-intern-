namespace ItiFinalProject.Interfaces.Services
{
    public interface IGenericService<TViewModel, TCreateViewModel, TUpdateViewModel>
    {
        public Task<IEnumerable<TViewModel>> GetAllAsync();
        public Task<TViewModel?> GetByIdAsync(int id);
        public Task<TUpdateViewModel?> GetForEditByIdAsync(int id);
        public Task CreateAsync(TCreateViewModel viewModel);
        public Task UpdateAsync(int id, TUpdateViewModel viewModel);
        public Task DeleteAsync(int id);
    }
}
