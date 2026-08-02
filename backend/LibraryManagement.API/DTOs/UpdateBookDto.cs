using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.API.DTOs
{
    public class UpdateBookDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string Author { get; set; } = "";


        [Required]
        [RegularExpression(
        "^(Available|Borrowed)$",
        ErrorMessage = "Status must be either Available or Borrowed.")]
        public string Status { get; set; } = "";
    }
}
