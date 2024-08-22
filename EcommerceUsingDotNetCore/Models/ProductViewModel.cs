using System.ComponentModel.DataAnnotations;

namespace EcommerceUsingDotNetCore.Models
{
    public class ProductViewModel
    {
        [Key]
        public int Id { get; set; }

        public string? PName { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public string? Category { get; set; }

        public IFormFile? Image { get; set; }
    }
}
