using HostData.Controller.Contract;

namespace HostData.Modules;

public class SessionModule : BaseModule
{
    private readonly ISessionController _sessionController;

    public SessionModule(ISessionController sessionController) : base()
    {
        _sessionController = sessionController;
    }
}