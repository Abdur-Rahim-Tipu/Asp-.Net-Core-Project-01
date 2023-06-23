using BookDetails.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookDetails.ViewModels
{
    public class BookInsertModel
    {
        public int BookId { get; set; }
        [Required, StringLength(50)]
        public string Title { get; set; } = default!;
        [Required, Column(TypeName = "date")]
        public DateTime PublishDate { get; set; }
        [Required]
        public int TotalPage { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal CoverPrice { get; set; }
        public bool IsStock { get; set; }
        [Required]
        public IFormFile CoverPage { get; set; } = default!;
        [Required, ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public virtual Publisher? Publisher { get; set; } = default!;
        public virtual List<Author> Authors { get; set; } = new List<Author>();
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}
