using LibraryManagement.Api.Services;
using LibraryManagement.API.DTOs;
using LibraryManagement.API.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using LibraryManagement.API.Enums;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;
    private readonly IMapper _mapper;

    public BooksController(BookService bookService,
                            IMapper mapper)
    {
        _bookService = bookService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _bookService.GetAllAsync();

        var result = _mapper.Map<List<BookDto>>(books);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var book = await _bookService.GetByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        var result = _mapper.Map<BookDto>(book);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
    {
        var book = _mapper.Map<Book>(dto);

        // Business rule: every new book begins as available.
        book.Status = BookStatus.Available;

        var createdBook = await _bookService.AddAsync(book);

        var result = _mapper.Map<BookDto>(createdBook);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdBook.Id },
            result
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
    [FromRoute] int id,
    [FromBody] UpdateBookDto dto)
    {
        var existingBook =
            await _bookService.GetByIdAsync(id);

        if (existingBook == null)
        {
            return NotFound();
        }

        _mapper.Map(dto, existingBook);

        var updatedBook =
            await _bookService.UpdateAsync(existingBook);

        var result =
            _mapper.Map<BookDto>(updatedBook);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deleted = await _bookService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}