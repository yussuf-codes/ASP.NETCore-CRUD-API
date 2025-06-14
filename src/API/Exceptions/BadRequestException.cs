namespace API.Exceptions;

public class BadRequestException : System.Exception
{
    public BadRequestException() : base("400 Error")
    {
    }
}
