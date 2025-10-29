using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Services;

namespace Api.Helpers
{
    public class HttpResult
    {
        public IActionResult ReturnCustomResponse(dynamic searchResult)
        {
            return new OkObjectResult(new
            {
                data = searchResult,
                statusCode = 200,
                Exception = ""
            });
        }

        public IActionResult ReturnCustomException(Exception ex)
        {
            return new ObjectResult(new
            {
                Exception = ex.Message,
                statusCode = 500,
                stackTrace = ex.StackTrace
            })
            {
                StatusCode = 500
            };
        }

        public IActionResult ReturnCustomErrors(List<string> errors)
        {
            return new BadRequestObjectResult(new
            {
                ErrorList = errors,
                statusCode = 400
            });
        }
    }
}
