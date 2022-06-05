using System.Net;
using System.Net.Sockets;

namespace Shared.Configuration;

public class NetOperation
{
    public IPAddress GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList.Where(ip => ip.AddressFamily.Equals(AddressFamily.InterNetwork)))
            return ip;

        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
}