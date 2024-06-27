using ExampleService.Domain.Interfaces;
using ExampleService.Domain.Models;

namespace ExampleService.Business.Services
{
    public class ExampleBusinessService(IExampleRepository exampleRepository) : IExampleService
    {
        public async Task<int> CreateDetailsExampleAsync(DetailsExample details)
        {
            return await exampleRepository.CreateDetailsExample(details);
        }
    }
}
