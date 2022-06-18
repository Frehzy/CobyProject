using HostData.Cache.Entities;
using Shared.Factory.Dto;

namespace HostData.Cache.Credentials;

public interface ICacheCredentials
{
    public IReadOnlyList<CredentialsAction> Credentials { get; }

    public Task<CredentialsDto> Add(string waiterPassword);

    public bool CheckCredentials(Guid credentialsId, out Guid waiterId);
}