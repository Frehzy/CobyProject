using HostData.Cache.Entities;
using HostData.Controller.Contract;
using Shared.Factory.Dto;
using System.Collections.Concurrent;

namespace HostData.Cache.Credentials;

public class CacheCredentials : ICacheCredentials
{
    private readonly ConcurrentDictionary<Guid, CredentialsAction> _credentials = new();
    private readonly IWaiterController _waiterController;

    public IReadOnlyList<CredentialsAction> Credentials => _credentials.Values.ToList();

    public CacheCredentials(IWaiterController waiterController)
    {
        _waiterController = waiterController;
    }

    public async Task<CredentialsDto> Add(string waiterPassword)
    {
        var waiterModel = await _waiterController.GetWaiterByPassword(waiterPassword);

        var credentials = new CredentialsAction(Guid.NewGuid(), waiterModel);
        credentials.Timeout += RemoveCredentials;
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