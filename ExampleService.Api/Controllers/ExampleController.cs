using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ExampleService.Api.Models.Responses;
using ExampleService.Domain.Interfaces;
using ExampleService.Api.Models.Requests;

namespace ExampleService.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class ExampleController(IExampleService exampleService, IMapper mapper) : ControllerBase
    {
        /// <summary>
        /// Creates Details in database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ///
        [Route("details")]
        [HttpPost]
        [ProducesResponseType(typeof(CreateDetailsExampleResponse), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(EmptyResult), 401)]
        [ProducesResponseType(typeof(EmptyResult), 403)]
        [ProducesResponseType(typeof(ErrorResponse), 422)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [Produces("application/json")]
        public async Task<ActionResult<CreateDetailsExampleResponse>> CreateDetailsExampleAsync(CreateDetailsExampleRequest request)
        {
            var details = mapper.Map<Domain.Models.DetailsExample>(request);
            var detailsId = await exampleService.CreateDetailsExampleAsync(details);

            var response = new CreateDetailsExampleResponse()
            {
                DetailsId = detailsId
            };

            return response;
        }
    }
}
