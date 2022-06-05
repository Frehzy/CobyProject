using System.Net;

namespace Api.Operations.NetOper;

public interface INetOperation
{
    IPAddress GetLocalIPAddress();
}