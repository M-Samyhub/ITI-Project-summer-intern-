namespace ItiFinalProject.View_Model.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string ImgePath { get; set; } = string.Empty;
        public string? ExistingImagePath { get; set; }

        // To Show The Department Name in The Table
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
