using HostData.Controller.Contract;
using Shared.Factory.Dto;

namespace HostData.Modules;

public class CredentialsModule : BaseModule
{
    private readonly ICredentialsController _credentialsController;

    public CredentialsModule(ICredentialsController credentialsController) : base()
    {
        _credentialsController = credentialsController;

#if DEBUG
        _credentialsController.CreateCredentials("ADMINPASSWORD");
#endif

        Get("/credentials/{password}", async parameters =>
        {
            var password = parameters.password;
            return await Execute<CredentialsDto>(Context, () => _credentialsController.CreateCredentials(password));
        });
    }
}