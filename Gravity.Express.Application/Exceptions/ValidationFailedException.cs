
namespace Gravity.Express.Application.Exceptions;

public class ValidationFailedException : Exception
{
    public ValidationFailedException(string errorMessage)
    {
        Errors = new Dictionary<string, string[]>() { { "Validation Exception", new[] { errorMessage } } };
    }
    public Dictionary<string, string[]> Errors { get; }
}
