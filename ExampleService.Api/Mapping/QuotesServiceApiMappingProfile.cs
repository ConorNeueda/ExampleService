using AutoMapper;
using ExampleService.Api.Models.Requests;
using ExampleService.Domain.Models;

namespace ExampleService.Api.Mapping
{
    public class ExampleServiceApiMappingProfile : Profile
    {
        public ExampleServiceApiMappingProfile() 
        {
            CreateMap<CreateDetailsExampleRequest, DetailsExample>();
        }
    }
}
