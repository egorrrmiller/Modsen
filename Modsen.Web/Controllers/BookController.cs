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
    public async Task<IActionResult> GetBooks(CancellationToken cancellationToken)
    {
        return Ok(await _bookRepository.GetBooks(cancellationToken));
    }

    [HttpGet("/getBook")]
    public async Task<IActionResult> GetBook(Guid? id, string? isbn, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBook(cancellationToken, id, isbn);

        if (book == null)
            return NotFound("Книга не найдена");

        return Ok(book);
    }

    [HttpPost("/addBook")]
    public async Task<IActionResult> AddBook(BookDto bookDto, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.AddBook(bookDto, cancellationToken);

        return Ok(book);
    }

    [HttpPut("/updateBook")]
    public async Task<IActionResult> UpdateBook(BookDto bookDto, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.UpdateBook(bookDto, cancellationToken);
        if (book == null)
            return NotFound("Книга не найдена");

        return Ok(book);
    }

    [HttpDelete("/deleteBook")]
    public async Task<IActionResult> DeleteBook(Guid? id, string? isbn, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.DeleteBook(cancellationToken, id, isbn);
        if (book == null)
            return NotFound("Книга не найдена");

        return Ok(book);
    }
}