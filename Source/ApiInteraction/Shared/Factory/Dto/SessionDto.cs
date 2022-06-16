namespace Shared.Factory.Dto;

public record SessionDto(Guid OrderId, List<OrderDto> Orders, int Version);