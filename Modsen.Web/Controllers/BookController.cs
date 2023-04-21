using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Database.Repository.Interfaces;
using Modsen.Domain.Dto;

namespace Modsen.Web.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet("/getBooks")]
    public async Task<IActionResult> GetBooks()
    {
        return Ok(_bookRepository.GetBooks());
    }

    [HttpGet("/getBook")]
    public async Task<IActionResult> GetBook(Guid? id, string? isbn, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var book = await _bookRepository.GetBookAsync(id, isbn, cancellationToken);

        if (book == null)
            return NotFound("Книга не найдена");

        return Ok(book);
    }

    [HttpPost("/addBook")]
    public async Task<IActionResult> AddBook(BookDto bookDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var book = await _bookRepository.AddBookAsync(bookDto, cancellationToken);

        return Ok(book);
    }

    [HttpPut("/updateBook")]
    public async Task<IActionResult> UpdateBook([FromQuery]Guid id, BookDto bookDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var book = await _bookRepository.UpdateBookAsync(id, bookDto, cancellationToken);
        if (book == null)
            return NotFound("Книга не найдена");

        return Ok(book);
    }

    [HttpDelete("/deleteBook")]
    public async Task<IActionResult> DeleteBook(Guid? id, string? isbn, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var book = await _bookRepository.DeleteBookAsync(id, isbn, cancellationToken);
        if (book == null)
            return NotFound("Книга не найдена");

        return Ok(book);
    }
}