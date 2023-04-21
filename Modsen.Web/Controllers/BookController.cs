using Microsoft.AspNetCore.Mvc;
using Modsen.Database.Repository.Interfaces;
using Modsen.Domain.Dto;

namespace Modsen.Web.Controllers;

[ApiController]
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
        return Ok(await _bookRepository.GetBooks());
    }

    [HttpGet("/getBook")]
    public async Task<IActionResult> GetBook(Guid? id, string? isbn)
    {
        var book = await _bookRepository.GetBook(id, isbn);

        if (book == null)
            return NotFound("Книга не найдена");

        return Ok(book);
    }

    [HttpPost("/addBook")]
    public async Task<IActionResult> AddBook(BookDto bookDto)
    {
        var book = await _bookRepository.AddBook(bookDto);

        if (book == null)
            return BadRequest("Книга с таким ISBN уже есть");

        return Ok(book);
    }

    [HttpPut("/updateBook")]
    public async Task<IActionResult> UpdateBook(BookDto bookDto)
    {
        var book = await _bookRepository.UpdateBook(bookDto);
        if (book == null)
            return NotFound("Книга не найдена");

        return Ok(book);
    }

    [HttpDelete("/deleteBook")]
    public async Task<IActionResult> DeleteBook(Guid? id, string? isbn)
    {
        var book = await _bookRepository.DeleteBook(id, isbn);
        if (book == null)
            return NotFound("Книга не найдена");

        return Ok(book);
    }
}