using System.ComponentModel.DataAnnotations;


namespace LibraryManagement.API.DTOs
{
    public class CreateBookDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = "";


        [Required]
        [StringLength(100)]
        public string Author { get; set; } = "";
    }
}
