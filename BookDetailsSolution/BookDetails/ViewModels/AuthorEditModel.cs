using BookDetails.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookDetails.ViewModels
{
    public class AuthorEditModel
    {
        public int AuthorId { get; set; }
        [Required, StringLength(50)]
        public string AuthorName { get; set; } = default!;
        [Required, Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
        [Required, StringLength(50)]
        public string WebsiteUrl { get; set; } = default!;
        [Required, StringLength(50)]
        public string Phone { get; set; } = default!;
        [Required, StringLength(50)]
        public string Email { get; set; } = default!;
        public IFormFile Picture { get; set; } = default!;
        [Required, ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book? Book { get; set; } = default!;
    }
}
