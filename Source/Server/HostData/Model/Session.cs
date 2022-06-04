namespace HostData.Model;

internal class Session
{
    public List<Order> Orders { get; set; }

    public int Version { get; set; }

    public Session() { }

    public Session(IEnumerable<Order> orders, int version)
    {
        Orders = orders.ToList();
        Version = version;
    }
}