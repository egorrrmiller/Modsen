using Modsen.Domain.Dto;
using Modsen.Domain.Models;

namespace Modsen.Database.Repository.Interfaces;

public interface IBookRepository
{
    Task<BookModel?> AddBook(BookDto bookDto);
    Task<List<BookModel>> GetBooks();
    Task<BookDto?> GetBook(Guid? id = null, string? isbn = null);
    Task<BookDto?> UpdateBook(BookDto bookDto);
    Task<BookDto?> DeleteBook(Guid? id = null, string? isbn = null);
}