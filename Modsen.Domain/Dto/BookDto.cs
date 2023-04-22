namespace Modsen.Domain.Dto;

public record BookDto(string Isbn, string Title, string Description, string Author, DateTime BorrowTime,
                      DateTime ReturnTime);