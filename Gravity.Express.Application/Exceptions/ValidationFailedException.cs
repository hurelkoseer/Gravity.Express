
namespace Gravity.Express.Application.Exceptions;

public class ValidationFailedException : Exception
{
    public ValidationFailedException(string errorMessage)
    {
        Errors = new Dictionary<string, string[]>() { { "Validation Exception", new[] { errorMessage } } };
    }

    public ValidationFailedException(string property, string errorMessage)
    {
        Errors = new Dictionary<string, string[]>() { { property, new[] { errorMessage } } };
    }

    public ValidationFailedException(string property, string[] errorMessages)
    {
        Errors = new Dictionary<string, string[]>() { { property, errorMessages } };
    }

    public ValidationFailedException(string property, string errorMessage, string errorCode)
    {
        Errors = new Dictionary<string, string[]>() { { property, new[] { errorCode,errorMessage } } };
    }

    public ValidationFailedException(Dictionary<string, string[]> errors)
    {
        Errors = errors;
    }

    public Dictionary<string, string[]> Errors { get; }
}
