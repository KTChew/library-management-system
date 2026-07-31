using LibraryManagement.API.Data;
using LibraryManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Services;

public class BookService
{
    private readonly LibraryDbContext _context;
    private readonly ILogger<BookService> _logger;

    public BookService(
        LibraryDbContext context,
        ILogger<BookService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all books.");

        var books = await _context.Books.ToListAsync();

        _logger.LogInformation(
            "Retrieved {BookCount} books.",
            books.Count);

        return books;
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        _logger.LogInformation(
            "Retrieving book with ID {BookId}.",
            id);

        var book = await _context.Books
            .FirstOrDefaultAsync(book => book.Id == id);

        if (book == null)
        {
            _logger.LogWarning(
                "Book with ID {BookId} was not found.",
                id);
        }

        return book;
    }

    public async Task<Book> AddAsync(Book book)
    {
        _logger.LogInformation(
            "Creating book with title {BookTitle}.",
            book.Title);

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Created book with ID {BookId}.",
            book.Id);

        return book;
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        _logger.LogInformation(
            "Updating book with ID {BookId}.",
            book.Id);

        _context.Books.Update(book);
        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Updated book with ID {BookId}.",
            book.Id);

        return book;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _logger.LogInformation(
            "Deleting book with ID {BookId}.",
            id);

        var book = await _context.Books
            .FirstOrDefaultAsync(book => book.Id == id);

        if (book == null)
        {
            _logger.LogWarning(
                "Cannot delete book with ID {BookId} because it was not found.",
                id);

            return false;
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Deleted book with ID {BookId}.",
            id);

        return true;
    }
}