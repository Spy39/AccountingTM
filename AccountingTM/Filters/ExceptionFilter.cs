using AccountingTM.Common;
using AccountingTM.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace AccountingTM.Middlewares
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            _logger.LogError($"При запроса произошла ошибка: {exception.Message}");

            var errorResponse = new ErrorResponse
            {
                Message = exception.Message
            };

            switch (exception)
            {
                case UserFriendlyException:
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Title = exception.GetType().Name;
                    break;

                case BadHttpRequestException:
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Title = exception.GetType().Name;
                    break;

                default:
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Title = "Internal Server Error";
                    break;
            }

            var httpContext = context.HttpContext;
            httpContext.Response.StatusCode = errorResponse.StatusCode;
            httpContext
              .Response
              .WriteAsJsonAsync(errorResponse).Wait();

        }
    }
}
