using ItiFinalProject.Models;
using ItiFinalProject.Services;
using ItiFinalProject.View_Model.Product;

namespace ItiFinalProject.Interfaces.Services
{
    public interface IProductService : IGenericService<ProductViewModel, CreateProductViewModel, UpdateProductViewModel>
    {
    }
}
