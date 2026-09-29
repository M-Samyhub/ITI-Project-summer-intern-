namespace ItiFinalProject.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;

        /* =============== Nav Property =============== */
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
