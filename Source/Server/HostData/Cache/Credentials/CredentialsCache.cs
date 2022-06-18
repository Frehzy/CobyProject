using HostData.Cache.Entities;
using HostData.Domain.Contracts.Models;
using Shared.Factory.Dto;
using System.Collections.Concurrent;

namespace HostData.Cache.Credentials;

public class CredentialsCache : ICredentialsCache
{
    private readonly ConcurrentDictionary<Guid, CredentialsAction> _credentials = new();

    public IReadOnlyList<CredentialsAction> Credentials => _credentials.Values.ToList();

    public async Task<CredentialsDto> Add(WaiterModel waiterModel)
    {
        var credentials = new CredentialsAction(waiterModel);
        credentials.TimerCallBackAction += RemoveCredentials;
        _credentials.TryAdd(credentials.CredentialsId, credentials);

        return new CredentialsDto(credentials.CredentialsId);
    }

    public bool CheckCredentials(Guid credentialsId, out Guid waiterId)
    {
        var returnValue = _credentials.TryGetValue(credentialsId, out var credentials);
        waiterId = credentials.Waiter.Id;
        return returnValue;
    }

    private void RemoveCredentials(CredentialsAction credentials) =>
        _credentials.Remove(credentials.CredentialsId, out _);
}