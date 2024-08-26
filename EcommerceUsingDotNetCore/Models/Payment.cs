using System.ComponentModel.DataAnnotations;

namespace EcommerceUsingDotNetCore.Models
{
    public class Payment
    {
        [Key]
        public int PId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public string? Category { get; set; }

        public string? Image { get; set; }

        public string? Suser { get; set; }

        public string? Date { get; set; }
    }
}
