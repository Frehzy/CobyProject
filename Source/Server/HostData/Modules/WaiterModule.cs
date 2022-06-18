using HostData.Controller.Contract;
using Shared.Factory.Dto;

namespace HostData.Modules;

public class WaiterModule : BaseModule
{
    private readonly IWaiterController _waiterController;

    public WaiterModule(IWaiterController waiterController) : base()
    {
        _waiterController = waiterController;

        Get("/waiters", parameters =>
        {
            return Execute(Context, () => _waiterController.GetWaiters());
        });

        Get("/waiter/{waiterId}", parameters =>
        {
            var waiterId = parameters.waiterId;
            return Execute<WaiterDto>(Context, () => _waiterController.GetWaiterById(waiterId));
        });

        Get("{credentialsId}/waiter/create/{name}/{password}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var name = parameters.name;
            var password = parameters.password;
            return Execute<WaiterDto>(Context, () => _waiterController.CreateWaiter(credentialsId, name, password));
        });

        Get("{credentialsId}/waiter/remove/{waiterId}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var waiterId = parameters.waiterId;
            return Execute<WaiterDto>(Context, () => _waiterController.RemoveWaiter(credentialsId, waiterId));
        });

        Get("{credentialsId}/waiter/update/addPermission/{waiterId}/{permission}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var waiterId = parameters.waiterId;
            var permission = parameters.permission;
            return Execute<WaiterDto>(Context, () => _waiterController.AddPermissionOnWaiterById(credentialsId, waiterId, permission));
        });

        Get("{credentialsId}/waiter/update/removePermission/{waiterId}/{permission}", parameters =>
        {
            var credentialsId = parameters.credentialsId;
            var waiterId = parameters.waiterId;
            var permission = parameters.permission;
            return Execute<WaiterDto>(Context, () => _waiterController.RemovePermissionOnWaiterById(credentialsId, waiterId, permission));
        });
    }
}