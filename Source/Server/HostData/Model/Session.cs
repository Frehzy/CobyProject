namespace HostData.Model;

internal class Session
{
    public Guid OrderId { get; set; }

    public List<Order> Orders { get; set; }

    public int Version { get; set; }

    public Session() { }

    public Session(Guid orderId, List<Order> orders, int version)
    {
        OrderId = orderId;
        Orders = orders;
        Version = version;
    }
}