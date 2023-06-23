using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookDetails.Models
{
    public class Publisher
    {
        public int PublisherId { get; set; }
        [Required, StringLength(50)]
        public string PublisherName { get; set; } = default!;
        [Required, StringLength(50), DataType(DataType.Url)]
        public string WebsiteUrl { get; set; } = default!;
        [Required, StringLength(50)]
        public string Phone { get; set; } = default!;
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        public virtual List<Book> Books { get; set; } = new List<Book>();
    }
    public class Book
    {
        public int BookId { get; set; }
        [Required, StringLength(50)]
        public string Title { get; set; } = default!;
        [Required, Column(TypeName = "date")]
        public DateTime PublishDate { get; set; } = DateTime.Now;
        [Required]
        public int TotalPage { get; set; }
        [Required, Column(TypeName = "money"), DataType(DataType.Date)]
        public decimal CoverPrice { get; set; }
        public bool IsStock { get; set; }
        public string CoverPage { get; set; } = default!;
        [Required, NotMapped]
        public IFormFile CoverImage { get; set; }=default!;
        [Required, ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public virtual Publisher? Publisher { get; set; } = default!;
        public virtual List<Author> Authors { get; set; } = new List<Author>();
        public virtual List<Order> Orders { get; set; } = new List<Order>();



    }
    public class Author
    {
        public int AuthorId { get; set; }
        [Required, StringLength(50)]
        public string AuthorName { get; set; } = default!;
        [Required, Column(TypeName = "date"), DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } = DateTime.Now;
        [Required, StringLength(50), DataType(DataType.Url)]
        public string WebsiteUrl { get; set; } = default!;
        [Required, StringLength(50)]
        public string Phone { get; set; } = default!;
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        
        public string? Picture { get; set; } = default!;
        [Required, NotMapped]
        public IFormFile AuthorImage { get; set; } = default!;
        [Required, ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book? Book { get; set; } = default!;
    }
    public class Order
    {
        public int OrderId { get; set; }
        [Required, StringLength(50)]
        public string CustomerName { get; set; } = default!;
        [Required, Column(TypeName = "date"), DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Discount { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required, ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book? Book { get; set; }
    }
    public class PublisherDbContext : DbContext
    {
        public PublisherDbContext(DbContextOptions<PublisherDbContext> options) : base(options) { }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
