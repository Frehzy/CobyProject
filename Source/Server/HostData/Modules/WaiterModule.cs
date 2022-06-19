using HostData.Controller.Contract;
using Shared.Factory.Dto;

namespace HostData.Modules;

public class WaiterModule : BaseModule
{
    private readonly IWaiterController _waiterController;

    public WaiterModule(IWaiterController waiterController) : base()
    {
        _waiterController = waiterController;

        Get("/waiters", async parameters =>
        {
            return await Execute(Context, () => _waiterController.GetWaiters());
        });

        Get("/waiter/{waiterId}", async parameters =>
        {
            var waiterId = parameters.waiterId;
            return await Execute<WaiterDto>(Context, () => _waiterController.GetWaiterById(waiterId));
        });

        Get("{credentialsId}/waiter/create/{name}/{password}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var name = parameters.name;
            var password = parameters.password;
            return await Execute<WaiterDto>(Context, () => _waiterController.CreateWaiter(credentialsId, name, password));
        });

        Get("{credentialsId}/waiter/remove/{waiterId}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var waiterId = parameters.waiterId;
            return await Execute<WaiterDto>(Context, () => _waiterController.RemoveWaiterById(credentialsId, waiterId));
        });

        Get("{credentialsId}/waiter/update/addPermission/{waiterId}/{permission}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var waiterId = parameters.waiterId;
            var permission = parameters.permission;
            return await Execute<WaiterDto>(Context, () => _waiterController.AddPermissionOnWaiterById(credentialsId, waiterId, permission));
        });

        Get("{credentialsId}/waiter/update/removePermission/{waiterId}/{permission}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var waiterId = parameters.waiterId;
            var permission = parameters.permission;
            return await Execute<WaiterDto>(Context, () => _waiterController.RemovePermissionOnWaiterById(credentialsId, waiterId, permission));
        });

        Get("{credentialsId}/waiter/personalSession/open/{waiterId}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var waiterId = parameters.waiterId;
            return await Execute<WaiterDto>(Context, () => _waiterController.OpenPersonalSession(credentialsId, waiterId));
        });

        Get("{credentialsId}/waiter/personalSession/close/{waiterId}", async parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var waiterId = parameters.waiterId;
            return await Execute<WaiterDto>(Context, () => _waiterController.ClosePersonalSession(credentialsId, waiterId));
        });
    }
}