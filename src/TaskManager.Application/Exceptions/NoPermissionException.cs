namespace TaskManager.Application.Exceptions;

public class NoPermissionException : Exception
{
    public NoPermissionException(string message)
        : base(message)
    {
    }
}