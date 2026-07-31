namespace LibraryManagement.API.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Author { get; set; } = "";

        public string Status { get; set; } = "";
    }
}
