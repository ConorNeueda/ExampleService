using ExampleService.Domain.Models;

namespace ExampleService.Domain.Interfaces
{
    public interface IExampleRepository
    {
        Task<int> CreateDetailsExample(DetailsExample details);
    }
}
