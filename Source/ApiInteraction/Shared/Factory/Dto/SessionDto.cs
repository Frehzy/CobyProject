namespace Shared.Factory.Dto;

internal class SessionDto
{
    public Guid OrderId { get; set; }

    public List<OrderDto> Orders { get; set; }

    public int Version { get; set; }

    public SessionDto() { }

    public SessionDto(Guid orderId, List<OrderDto> orders, int version)
    {
        OrderId = orderId;
        Orders = orders;
        Version = version;
    }
}