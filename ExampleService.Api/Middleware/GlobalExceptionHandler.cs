using ExampleService.Domain.Exceptions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ExampleService.Api.Middleware
{
    public class GlobalExceptionHandler(RequestDelegate requestDelegate, ILogger<GlobalExceptionHandler> logger)
    {
        private readonly RequestDelegate _requestDelegate = requestDelegate;
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;


        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext != null)
            {
                try
                {
                    await _requestDelegate(httpContext).ConfigureAwait(false);
                }
                catch (NotFoundException ex)
                {
                    await HandleExceptionAsync(httpContext, HttpStatusCode.NotFound, ex).ConfigureAwait(false);
                }
                catch (UnprocessableEntityException ex)
                {
                    await HandleExceptionAsync(httpContext, HttpStatusCode.UnprocessableEntity, ex).ConfigureAwait(false);
                }
                catch (NoContentException ex)
                {
                    await HandleExceptionAsync(httpContext, HttpStatusCode.NoContent, ex).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "{ExceptionMessage}", ex.Message);

                    await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex).ConfigureAwait(false);
                }
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, Exception exception)
        {
            string result = JsonSerializer.Serialize(new Models.Responses.ErrorResponse
            {
                Error = GetAllErrorMessages(exception)
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(result);
        }

        private static string GetAllErrorMessages(Exception exception)
        {
            var errorMessageBuilder = new StringBuilder();

            errorMessageBuilder.Append(exception.Message);

            var innerException = exception.InnerException;

            while (innerException != null)
            {
                errorMessageBuilder.Append($", INNER EXCEPTION: {innerException.Message}");
            }

            return errorMessageBuilder.ToString();
        }
    }
}
