using System.ComponentModel.DataAnnotations;

namespace BookDetails.ViewModels
{
    public class BookCoverPageUpload
    {
            [Required]
            public IFormFile CoverPage { get; set; } = default!;

    }
}
