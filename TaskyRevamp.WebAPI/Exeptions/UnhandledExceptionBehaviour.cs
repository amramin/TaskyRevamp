using TaskyRevamp.WebAPI.FileLogger;
using MediatR;

namespace TaskyRevamp.WebAPI.Exeptions;


public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, " Request: Unhandled Exception for Request {Name}", requestName);

            LogToFile(ex, requestName);

            throw;
        }
    }

    public void LogToFile(Exception ex, string requestName)
    {
        //Define the path to the text file
        string logFilePath = "console_log.txt";

        //Create a StreamWriter to write logs to a text file
        using (StreamWriter logFileWriter = new StreamWriter(logFilePath, append: true))
        {
            //Create an ILoggerFactory
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                //Add console output
                builder.AddSimpleConsole(options =>
                {
                    options.IncludeScopes = true;
                    options.SingleLine = true;
                    options.TimestampFormat = "HH:mm:ss ";
                });

                //Add a custom log provider to write logs to text files
                builder.AddProvider(new CustomFileLoggerProvider(logFileWriter));
            });

            //Create an ILogger
            ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

            // Output some text on the console
            using (logger.BeginScope("[scope is enabled]"))
            {
                //logger.LogInformation("Hello World!");
                //logger.LogInformation("Logs contain timestamp and log level.");
                //logger.LogInformation("Each log message is fit in a single line.");
                //logger.LogInformation($"{requestName}");
                //logger.LogInformation($"{ex}");
                LoggingUtility.LogException(ex, requestName);
            }
        }
    }

}