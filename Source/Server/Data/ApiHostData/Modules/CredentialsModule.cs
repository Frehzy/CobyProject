using ApiHostData.Controller.Contract;
using Shared.Factory.Dto;
using SharedData.Modules;

namespace ApiHostData.Modules;

public class CredentialsModule : BaseModule
{
    private readonly ICredentialsController _credentialsController;

    public CredentialsModule(ICredentialsController credentialsController) : base()
    {
        _credentialsController = credentialsController;

#if DEBUG
        _credentialsController.CreateCredentials("ADMINPASSWORD");
#endif

        Get("/credentials/create/{password}", async parameters =>
        {
            var password = parameters.password;
            return await Execute<CredentialsDto>(Context, () => _credentialsController.CreateCredentials(password));
        });

        Get("/moduleLicence/check/{organizationId}/{moduleLicenceId}", async parameters =>
        {
            var organizationId = parameters.organizationId;
            var moduleLicenceId = parameters.moduleLicenceId;
            return await Execute<List<LicenceDto>>(Context, () => _credentialsController.CheckLicence(organizationId, moduleLicenceId));
        });

        Get("/session/create/{orderId}", async parameters =>
        {
            var orderId = parameters.orderId;
            return await Execute<SessionDto>(Context, () => _credentialsController.CreateSession(orderId));
        });
    }
}