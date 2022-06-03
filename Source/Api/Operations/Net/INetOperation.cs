using System.Net;

namespace Api.Operations.Net
{
    public interface INetOperation
    {
        IPAddress GetLocalIPAddress();
    }
}