
using System.ComponentModel.DataAnnotations;

namespace BookDetails.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
