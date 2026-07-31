using LibraryManagement.Api.Services;
using LibraryManagement.API.DTOs;
using LibraryManagement.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _bookService.GetAllAsync();

        var result = books.Select(book => new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Status = book.Status
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        var result = new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Status = book.Status
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookDto dto)
    {
        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            Status = "Available"
        };

        var createdBook = await _bookService.AddAsync(book);

        var result = new BookDto
        {
            Id = createdBook.Id,
            Title = createdBook.Title,
            Author = createdBook.Author,
            Status = createdBook.Status
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdBook.Id },
            result
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateBookDto dto)
    {
        var existingBook = await _bookService.GetByIdAsync(id);

        if (existingBook == null)
        {
            return NotFound();
        }

        existingBook.Title = dto.Title;
        existingBook.Author = dto.Author;
        existingBook.Status = dto.Status;

        var updatedBook = await _bookService.UpdateAsync(existingBook);

        var result = new BookDto
        {
            Id = updatedBook.Id,
            Title = updatedBook.Title,
            Author = updatedBook.Author,
            Status = updatedBook.Status
        };

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _bookService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}