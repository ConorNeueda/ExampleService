using ExampleService.Domain.Models;

namespace ExampleService.Domain.Interfaces
{
    public interface IExampleService
    {
        Task<int> CreateDetailsExampleAsync(DetailsExample details);
    }
}
