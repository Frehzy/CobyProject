using HostData.Cache.Entities;
using HostData.Domain.Contracts.Models;
using HostData.System.Collections.Concurrent;
using Serilog;
using Shared.Factory.Dto;
using System.Collections.Specialized;
using System.Text.Json;

namespace HostData.Cache.Credentials;

public class CredentialsCache : ICredentialsCache
{
    private readonly ObservableConcurrentDictionary<Guid, CredentialsAction> _credentials = new();

    public IReadOnlyList<CredentialsAction> Credentials => _credentials.Values.ToList();

    public CredentialsCache()
    {
        _credentials.CollectionChanged += Credentials_CollectionChanged;
    }

    public async Task<CredentialsDto> TryAdd(WaiterModel waiterModel)
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

    private void Credentials_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
            foreach (CredentialsAction newItem in e.NewItems)
                Log.Information($"{nameof(CredentialsCache)}. Added item: {JsonSerializer.Serialize(newItem, Json.Options.JsonSerializerOptions)}");

        if (e.OldItems != null)
            foreach (CredentialsAction oldItem in e.OldItems)
                Log.Information($"{nameof(CredentialsCache)}. Remove item: {JsonSerializer.Serialize(oldItem, Json.Options.JsonSerializerOptions)}");
    }

    private void RemoveCredentials(CredentialsAction credentials) =>
        _credentials.TryRemove(credentials.CredentialsId, out _);
}