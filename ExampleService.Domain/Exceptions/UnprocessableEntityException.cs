namespace ExampleService.Domain.Exceptions
{
    public class UnprocessableEntityException : Exception
    {
        // Default constructor
        public UnprocessableEntityException()
        {
        }

        // Constructor that accepts a specific message
        public UnprocessableEntityException(string message)
            : base(message)
        {
        }
    }
}
