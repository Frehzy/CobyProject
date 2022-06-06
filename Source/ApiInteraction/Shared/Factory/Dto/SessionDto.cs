namespace Shared.Factory.Dto;

internal record SessionDto(Guid OrderId, List<OrderDto> Orders, int Version);