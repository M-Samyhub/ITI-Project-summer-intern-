using AutoMapper;
using ItiFinalProject.Interfaces.Repositories;
using ItiFinalProject.Interfaces.Services;
using ItiFinalProject.Models;
using ItiFinalProject.View_Model.Product;

namespace ItiFinalProject.Services
{
    public class ProductService : GenericService<Product, ProductViewModel, CreateProductViewModel, UpdateProductViewModel>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductService(IWebHostEnvironment webHostEnvironment, IProductRepository productRepository, IGenericRepository<Product> repository, IMapper mapper) : base(repository, mapper)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public override async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var products = await _productRepository.GetAllWithCategoryAsync();
            return _mapper.Map<IEnumerable<ProductViewModel>>(products);
        }

        public override async Task<ProductViewModel?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdWithCategory(id);
            if (product == null)
                return null;

            return _mapper.Map<ProductViewModel>(product);

        }
        public override async Task<UpdateProductViewModel?> GetForEditByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            return new UpdateProductViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Quantity = product.Quantity,
                
                ExistingImagePath = product.ImgePath
            };
        }

        public override async Task CreateAsync(CreateProductViewModel viewModel)
        {
            var product = _mapper.Map<Product>(viewModel);

            if (viewModel.ImgePath != null && viewModel.ImgePath.Length > 0)
            {
                product.ImgePath = await UploadImageAsync(viewModel.ImgePath);
            }
            else
            {
                product.ImgePath = "/images/products/default.png";
            }

            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
        }
        public override async Task UpdateAsync(int id, UpdateProductViewModel updateViewModel)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return;

            string oldDbImagePath = product.ImgePath;

            _mapper.Map(updateViewModel, product);

            if (updateViewModel.ImgePath != null && updateViewModel.ImgePath.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + updateViewModel.ImgePath.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await updateViewModel.ImgePath.CopyToAsync(fileStream);
                }

                string imageToDelete = !string.IsNullOrEmpty(updateViewModel.ExistingImagePath)
                    ? updateViewModel.ExistingImagePath
                    : oldDbImagePath;

                if (!string.IsNullOrEmpty(imageToDelete))
                {
                    string oldFileName = Path.GetFileName(imageToDelete);
                    string oldFilePath = Path.Combine(uploadsFolder, oldFileName);
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }

                product.ImgePath = uniqueFileName;
            }
            else
            {
                string fileNameToKeep = !string.IsNullOrEmpty(updateViewModel.ExistingImagePath)
                    ? updateViewModel.ExistingImagePath
                    : oldDbImagePath;

                product.ImgePath = !string.IsNullOrEmpty(fileNameToKeep)
                    ? Path.GetFileName(fileNameToKeep)
                    : null;
            }

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
        }
        private async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return $"/images/products/{uniqueFileName}";
        }
    }
}
