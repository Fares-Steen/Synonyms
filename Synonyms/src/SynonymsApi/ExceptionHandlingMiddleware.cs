using Entities.Exceptions;
using System.Net;

namespace SynonymsApi
{
    public class ExceptionHandlingMiddleware: IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (DomainDuplicationException e)
            {
                await LogTheError(e, context);
                context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                await context.Response.WriteAsync(e.Message);
            }
            catch (DomainNotValidException e)
            {
                await LogTheError(e, context);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(e.Message);
            }
            catch (Exception e)
            {
                await LogTheError(e, context);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("");
            }
        }
        private async Task LogTheError(Exception e, HttpContext context)
        {
            if (e.InnerException != null) _logger.LogError("Exception: {EMessage} \\r\\n InnerException: {EInnerException} \\r\\n StackTrace: {EStackTrace}", e.Message, e.InnerException, e.StackTrace);
        }
    }
}
