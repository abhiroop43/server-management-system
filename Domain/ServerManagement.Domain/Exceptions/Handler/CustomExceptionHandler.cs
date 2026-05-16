using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ServerManagement.Domain.Exceptions.Handler;

public class CustomExceptionHandler(
    ILogger<CustomExceptionHandler> logger,
    IWebHostEnvironment environment
) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        logger.LogError("Exception {0} occurred on: {1}", exception.Message, DateTime.UtcNow);

        string detail, title;

        switch (exception)
        {
            case ValidationException:
            case BadRequestException:
                detail = exception.Message;
                title = exception.GetType().Name;
                break;

            default:

        }

        return true;
    }
}
