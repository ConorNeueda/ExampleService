using System.ComponentModel.DataAnnotations;

namespace ExampleService.Api.Models.Requests
{
    public class CreateDetailsExampleRequest
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set;}
        [Required]
        public string? Email { get; set;}
        public string? Phone { get; set;}

    }
}
