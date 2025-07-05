using Domain.Exceptions;
using Shared.Errors;

namespace Store.API.Middelware
{
    public class GlobalHandleErroeMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalHandleErroeMiddleware> logger;

        public GlobalHandleErroeMiddleware(RequestDelegate _next,ILogger<GlobalHandleErroeMiddleware> logger)
        {
            next = _next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
                if (context.Response.StatusCode == 404)
                {
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetails()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        ErrorMessage = $"EndPoint {context.Request.Path} Is Not Found"
                    };
                    await context.Response.WriteAsJsonAsync(response);
                }

            }catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);
                context.Response.ContentType = "application/json";
                var response = new ErrorDetails()
                {
                    ErrorMessage = ex.Message
                };


                response.StatusCode = ex switch
                {
                   NotFoundExceptions => StatusCodes.Status404NotFound,
                   _ => StatusCodes.Status500InternalServerError,
                };
                context.Response.StatusCode= response.StatusCode;
                await context.Response.WriteAsJsonAsync(response);

            }
        }
    }
}
