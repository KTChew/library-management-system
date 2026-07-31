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
        [StringLength(20)]
        public string Status { get; set; } = "";
    }
}
