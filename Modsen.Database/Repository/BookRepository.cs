using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modsen.Database.Context;
using Modsen.Database.Exceptions;
using Modsen.Database.Repository.Interfaces;
using Modsen.Domain.Dto;
using Modsen.Domain.Models;
using Modsen.Mapper.Extensions;

namespace Modsen.Database.Repository;

public class BookRepository : IBookRepository
{
    private readonly ModsenContext _context;

    private readonly ILogger<BookRepository> _logger;

    private readonly IMapper _mapper;

    public BookRepository(ModsenContext context, IMapper mapper, ILogger<BookRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BookModel?> AddBookAsync(BookDto bookDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var book = _context.Books.AsNoTracking()
            .FirstOrDefault(isbn => isbn.Isbn == bookDto.Isbn);

        if (book != null)
        {
            throw new BookExistsException("Книга с таким ISBN уже есть");
        }

        book = bookDto.MapToModel(_mapper);
        var entity = await _context.Books.AddAsync(book, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Entity;
    }

    public List<BookModel> GetBooks() => _context.Books.AsNoTracking()
        .ToList();

    public async Task<BookDto?> GetBookAsync(Guid? id = null, string? isbn = null,
                                             CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var books = _context.Books;

        var book = await books.FindAsync(new object?[]
        {
            id,
            isbn
        }, cancellationToken);

        return book?.MapToDto(_mapper);
    }

    public async Task<BookDto?> UpdateBookAsync(Guid id, BookDto bookDto, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var book = _context.Books.AsNoTracking()
            .FirstOrDefault(book => book.Id == id);

        if (book == null)
        {
            return null;
        }

        var entity = _context.Books.Update(bookDto.MapToModel(_mapper));
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation($"Обновлена информация о книге с Id {entity.Entity.Id}");

        return entity.Entity.MapToDto(_mapper);
    }

    public async Task<BookDto?> DeleteBookAsync(Guid? id = null, string? isbn = null,
                                                CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var books = _context.Books;
        var book = books.FirstOrDefault(book => book.Id == id || book.Isbn == isbn);

        if (book == null)
        {
            return null;
        }

        var entity = _context.Books.Remove(book);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation($"Книга с Id {entity.Entity.Id} была удалена");

        return book.MapToDto(_mapper);
    }
}