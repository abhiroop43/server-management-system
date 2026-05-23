using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace ServerManagement.Domain.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation(
            "[START] Handle Request={Request} - Response={Response} - RequestData={RequestData}",
            typeof(TRequest).Name,
            typeof(TResponse).Name,
            request
        );

        var timer = new Stopwatch();
        timer.Start();

        var response = await next(cancellationToken);

        timer.Stop();

        var timeTaken = timer.Elapsed;

        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning(
                "[PERFORMANCE] The reqyest {Request} took {TimeTaken} seconds",
                typeof(TRequest).Name,
                timeTaken.Seconds
            );
        }

        logger.LogInformation(
            "[END] Handled {Request} with {Response}",
            typeof(TRequest),
            typeof(TResponse).Name
        );

        return response;
    }
}
