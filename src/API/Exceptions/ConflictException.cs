namespace API.Exceptions;

public class ConflictException : System.Exception
{
    public ConflictException() : base("409 Error")
    {
    }
}
