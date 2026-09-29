using Microsoft.AspNetCore.Mvc.Rendering;

namespace ItiFinalProject.View_Model.Product
{
    public class CreateProductViewModel
    {
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public IFormFile? ImgePath { get; set; } 
        public int? CategoryId { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}
