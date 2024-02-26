namespace Gravity.Express.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string name, string message)
            : base($"Entity \"{name}\" was not found. Detail : {message}")
        {
        }
    }
}
