namespace ItiFinalProject.View_Model.Product
{
    public class UpdateProductViewModel : CreateProductViewModel
    {
        public int Id { get; set; }
        public string? ExistingImagePath { get; set; }
    }
}
