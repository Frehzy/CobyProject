using ApiHostData.Cache.Entities;
using ApiHostData.Domain.Models;
using Shared.Factory.Dto;

namespace ApiHostData.Cache.Credentials;

public interface ICredentialsCache
{
    public IReadOnlyList<CredentialsAction> Credentials { get; }

    public Task<CredentialsDto> TryAdd(WaiterModel waiterModel);

    public bool CheckCredentials(Guid credentialsId, out Guid waiterId);
}