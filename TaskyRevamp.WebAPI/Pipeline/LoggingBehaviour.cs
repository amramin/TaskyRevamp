using System.Diagnostics;
using System.Reflection;
using MediatR;

namespace TaskyRevamp.WebAPI.Pipeline;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>

{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)

    {
        var stopwatch = Stopwatch.StartNew();
        var requestName = request.GetType().Name;
        var requestGuid = Guid.NewGuid().ToString();

        var requestNameWithGuid = $"{requestName} [{requestGuid}]";

        _logger.LogInformation($"[START] {requestNameWithGuid}");
        TResponse response;

        try
        {
            Type myType = request.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(request, null);
                _logger.LogInformation("[PROPS] {Property} : {@Value}", prop.Name, propValue);
            }
            response = await next();
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation(
                $"[END] {requestNameWithGuid}; Execution time={stopwatch.ElapsedMilliseconds}ms");
        }

        return response;
    }
}