namespace ItiFinalProject.Models
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public decimal Price {  get; set; }
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string ImgePath { get; set; } = string.Empty;

        /* =============== Nav Property =============== */
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
