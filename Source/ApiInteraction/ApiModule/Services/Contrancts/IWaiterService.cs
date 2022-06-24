using ApiModule.Services;
using Shared.Data.Enum;
using Shared.Factory.Dto;

namespace ApiModule.Services.Contrancts;

internal interface IWaiterService : IBaseService<WaiterDto>
{
    Task SendWaiter(WaiterDto waiter, EventType eventType);
}