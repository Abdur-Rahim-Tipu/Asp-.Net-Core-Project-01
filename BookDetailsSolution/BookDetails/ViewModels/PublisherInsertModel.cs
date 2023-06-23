using BookDetails.Models;
using System.ComponentModel.DataAnnotations;

namespace BookDetails.ViewModels
{
    public class PublisherInsertModel
    {
            public int PublisherId { get; set; }
            [Required, StringLength(50)]
            public string PublisherName { get; set; } = default!;
            [Required, StringLength(50)]
            public string WebsiteUrl { get; set; } = default!;
            [Required, StringLength(50)]
            public string Phone { get; set; } = default!;
            [Required, StringLength(50)]
            public string Email { get; set; } = default!;
            public virtual List<Book> Books { get; set; } = new List<Book>();
            public virtual List<Author> Authors { get; set; } = new List<Author>();
    }
}
