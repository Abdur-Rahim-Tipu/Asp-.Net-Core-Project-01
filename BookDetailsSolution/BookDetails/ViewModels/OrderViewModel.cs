using BookDetails.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookDetails.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        [Required, StringLength(50)]
        public string CustomerName { get; set; } = default!;
        [Required, Column(TypeName = "date")]
        public DateTime OrderDate { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Discount { get; set; }
        [Required]
        public int Quantity { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal NetPay { get; set; }
        public string BookTitle { get; set; } = default!;
        [Required, ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book? Book { get; set; }
    }
}
