using Shared.Data.Enum;
using Shared.Factory.Dto;

namespace ApiModule.Services.Contrancts;

internal interface IOrderService : IBaseService<OrderDto>
{
    Task SendOrder(OrderDto order, EventType eventType);
}