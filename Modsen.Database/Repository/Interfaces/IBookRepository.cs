using Modsen.Domain.Dto;
using Modsen.Domain.Models;

namespace Modsen.Database.Repository.Interfaces;

public interface IBookRepository
{
    Task<BookModel?> AddBook(BookDto bookDto, CancellationToken cancellationToken);
    Task<List<BookModel>> GetBooks(CancellationToken cancellationToken);
    Task<BookDto?> GetBook(CancellationToken cancellationToken, Guid? id = null, string? isbn = null);
    Task<BookDto?> UpdateBook(BookDto bookDto, CancellationToken cancellationToken);
    Task<BookDto?> DeleteBook(CancellationToken cancellationToken, Guid? id = null, string? isbn = null);
}