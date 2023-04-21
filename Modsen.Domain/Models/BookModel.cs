namespace Modsen.Domain.Models;

public class BookModel
{
    public Guid Id { get; init; }
    public string Isbn { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Author { get; init; }
    public DateTime BorrowTime { get; init; }
    public DateTime ReturnTime { get; init; }
}