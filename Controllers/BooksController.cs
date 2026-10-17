using BooksApiDemo.DTOs;
using BooksApiDemo.Models;
using BooksApiDemo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksApiDemo.Controllers;

[ApiController]
[Authorize]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BooksController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    [Authorize(Policy = "read:books")]
    public async Task<ActionResult<IEnumerable<Book>>> GetAll()
    {
        var books = await _bookRepository.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = "read:books")]
    public async Task<ActionResult<Book>> GetById(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book is null)
            return NotFound();

        return Ok(book);
    }

    [HttpPost]
    [Authorize(Policy = "create:books")]
    public async Task<ActionResult<Book>> Create(CreateBookRequest request)
    {
        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            PublicationYear = request.PublicationYear
        };

        book.Id = await _bookRepository.CreateAsync(book);

        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "update:books")]
    public async Task<IActionResult> Update(int id, UpdateBookRequest request)
    {
        var book = new Book
        {
            Id = id,
            Title = request.Title,
            Author = request.Author,
            PublicationYear = request.PublicationYear
        };

        var updated = await _bookRepository.UpdateAsync(book);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "delete:books")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _bookRepository.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
