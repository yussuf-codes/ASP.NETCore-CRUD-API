namespace API.Exceptions;

public class NotFoundException : System.Exception
{
    public NotFoundException() : base("404 Error")
    {
    }
}
