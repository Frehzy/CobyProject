using Microsoft.Extensions.Logging;

namespace HostData.Controllers.LogFactory;

internal abstract class LoggerController : LoggerComponent
{
    public LoggerController(ILogger logger)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override void Log(LogLevel level, string message)
    {
        Logger.Log(level, message);
    }
}