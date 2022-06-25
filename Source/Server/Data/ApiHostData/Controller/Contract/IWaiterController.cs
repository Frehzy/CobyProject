using ApiHostData.Domain.Models;
using Shared.Factory.Dto;

namespace ApiHostData.Controller.Contract;

public interface IWaiterController
{
    public Task<List<WaiterDto>> GetWaiters();

    public Task<WaiterDto> GetWaiterById(dynamic waiterId);

    [Obsolete("ТОЛЬКО ДЛЯ СЕРВЕРА")]
    public Task<WaiterModel> GetWaiterByPassword(dynamic password);

    public Task<WaiterDto> CreateWaiter(dynamic credentials, dynamic name, dynamic password);

    public Task<WaiterDto> RemoveWaiterById(dynamic credentials, dynamic waiterId);

    public Task<WaiterDto> AddPermissionOnWaiterById(dynamic credentials, dynamic waiterId, dynamic permissionId);

    public Task<WaiterDto> RemovePermissionOnWaiterById(dynamic credentials, dynamic waiterId, dynamic permissionId);

    public Task<WaiterDto> OpenPersonalShift(dynamic credentials, dynamic waiterId);

    public Task<WaiterDto> ClosePersonalShift(dynamic credentials, dynamic waiterId);
}