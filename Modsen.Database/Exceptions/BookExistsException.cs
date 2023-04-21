namespace Modsen.Database.Exceptions;

public class BookExistsException : Exception
{
    public BookExistsException(string message) : base(message)
    {
    }

    public BookExistsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}