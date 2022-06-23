using Shared.Factory.Dto;

namespace Api.Services.Contrancts;

internal interface IWaiterService : IBaseService<WaiterDto>
{
    Task SendWaiter(WaiterDto waiter);
}