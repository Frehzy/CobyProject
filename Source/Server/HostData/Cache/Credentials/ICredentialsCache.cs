using HostData.Cache.Entities;
using HostData.Domain.Contracts.Models;
using Shared.Factory.Dto;

namespace HostData.Cache.Credentials;

public interface ICredentialsCache
{
    public IReadOnlyList<CredentialsAction> Credentials { get; }

    public Task<CredentialsDto> Add(WaiterModel waiterModel);

    public bool CheckCredentials(Guid credentialsId, out Guid waiterId);
}