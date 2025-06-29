namespace API.Exceptions;

public class UnauthorizedException : System.Exception
{
    public UnauthorizedException() : base("401 Error")
    {
    }
}
