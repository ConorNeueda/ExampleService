using ExampleService.Domain.Interfaces;
using ExampleService.Domain.Models;

namespace ExampleService.Persistence.Repositories
{
    public class ExampleRepository : IExampleRepository
    {
        public Task<int> CreateDetailsExample(DetailsExample details)
        {
            throw new NotImplementedException();
        }
    }
}
