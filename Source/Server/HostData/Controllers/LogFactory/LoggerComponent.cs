using Microsoft.Extensions.Logging;

namespace HostData.Controllers.LogFactory;

internal abstract class LoggerComponent
{
    public ILogger Logger { get; protected set; }

    public abstract void Log(LogLevel level, string message);
}