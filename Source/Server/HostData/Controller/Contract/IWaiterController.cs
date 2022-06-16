using Shared.Factory.Dto;

namespace HostData.Controller.Contract;

public interface IWaiterController
{
    public Task<WaiterDto> CreateWaiter(string name, string password);

    public Task<List<WaiterDto>> GetWaiters();
}