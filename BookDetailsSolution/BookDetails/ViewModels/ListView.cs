namespace BookDetails.ViewModels
{
    public class ListView
    {
        public int PublisherId { get; set; }
        public string PublisherName { get; set; } = default!;
        public string Title { get; set; } = default!;
        public DateTime PublishDate { get; set; }
        public decimal Price { get; set; }
        public string CoverPage { get; set; }= default!;
        public string AuthorPic { get; set; } = default!;
        public string Contract { get; set; } = default!;
        public string AuthorName { get; set; } = default!;
        
    }
}
