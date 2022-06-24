using Shared.Data.Enum;
using Shared.Factory.Dto;

namespace Api.Services.Contrancts;

internal interface IOrderService : IBaseService<OrderDto>
{
    Task SendOrder(OrderDto order, EventType eventType);
}