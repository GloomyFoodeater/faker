namespace Faking.Core;

public class FakerException: Exception
{
    public string Message { get; }
    
    public FakerException(string message)
    {
        Message = message;
    }
}