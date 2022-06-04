namespace Api.Factory.Dto;

internal class SessionDto
{
    public List<OrderDto> Orders { get; set; }

    public int Version { get; set; }

    public SessionDto() { }

    public SessionDto(List<OrderDto> orders, int version)
    {
        Orders = orders;
        Version = version;
    }
}