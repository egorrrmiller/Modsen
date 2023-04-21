using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    private readonly IMapper _mapper;

    public BookRepository(ModsenContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BookModel?> AddBook(BookDto bookDto, CancellationToken cancellationToken)
    {
        var book = await _context.Books.FindAsync(new object?[] { bookDto.Isbn }, cancellationToken);

        if (book != null)
            throw new BookExistsException("Книга с таким ISBN уже есть");

        book = bookDto.MapToModel(_mapper);
        var entity = await _context.Books.AddAsync(book!, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Entity;
    }

    public async Task<List<BookModel>> GetBooks(CancellationToken cancellationToken)
    {
        return await _context.Books.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<BookDto?> GetBook(CancellationToken cancellationToken, Guid? id = null, string? isbn = null)
    {
        var books = _context.Books;
        var book = await books.FindAsync(new object?[] { id }, cancellationToken) ??
                   await books.FindAsync(new object?[] { isbn }, cancellationToken);

        return book?.MapToDto(_mapper);
    }

    public async Task<BookDto?> UpdateBook(BookDto bookDto, CancellationToken cancellationToken)
    {
        var book = await _context.Books.FindAsync(new object?[] { bookDto.Isbn }, cancellationToken);

        if (book == null)
            return null;

        var entity = _context.Books.Update(bookDto.MapToModel(_mapper));
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Entity.MapToDto(_mapper);
    }

    public async Task<BookDto?> DeleteBook(CancellationToken cancellationToken, Guid? id = null, string? isbn = null)
    {
        var books = _context.Books;
        var book  = await books.FindAsync(id) ?? await books.FindAsync(isbn);

        if (book == null)
            return null;

        var entity = _context.Books.Remove(book);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Entity.MapToDto(_mapper);
    }
}