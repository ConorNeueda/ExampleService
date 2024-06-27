namespace ExampleService.Domain.Exceptions
{
    public class NoContentException : Exception
    {
        public NoContentException()
        {
        }

        // Constructor that accepts a specific message
        public NoContentException(string message)
            : base(message)
        {
        }
    }
}
