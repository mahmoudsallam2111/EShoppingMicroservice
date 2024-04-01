using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Behavoirs
{
    public class LoggingBehavoir<TRequest, TResponse>(ILogger<LoggingBehavoir<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation($"[Start] handle={typeof(TRequest).Name} - response={typeof(TResponse).Name}  - requesDate={request}");

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();
            var timeTaken = timer.Elapsed;  
            timer.Stop();
            if (timeTaken.Seconds > 3 )
            {
                logger.LogWarning($"[Performance Issue] the request {typeof(TRequest).Name} take more than {timeTaken}");
            }

            logger.LogWarning($"[End] Handle request {typeof(TRequest).Name} with {typeof(TResponse).Name}");

            return response;
        }
    }
}
