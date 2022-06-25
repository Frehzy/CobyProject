using ApiHostData.Cache.Entities;
using ApiHostData.Domain.Models;
using Serilog;
using Shared.Factory.Dto;
using SharedData.System.Collections.Concurrent;
using SharedData.System.Text.Json;
using System.Collections.Specialized;
using System.Text.Json;

namespace ApiHostData.Cache.Credentials;

public class CredentialsCache : ICredentialsCache, IDisposable
{
    private readonly ObservableConcurrentDictionary<Guid, CredentialsAction> _credentials = new();

    public IReadOnlyList<CredentialsAction> Credentials => _credentials.Values.ToList();

    public CredentialsCache()
    {
        _credentials.CollectionChanged += Credentials_CollectionChanged;
    }

    public async Task<CredentialsDto> TryAdd(WaiterModel waiterModel)
    {
        if (CheckIfExistsCredentials(waiterModel.Id, out var existsCredentials) is not null)
        {
            existsCredentials.ResetTimer();
            return new CredentialsDto(existsCredentials.CredentialsId);
        }

        var credentials = new CredentialsAction(waiterModel);
        credentials.TimerCallBackAction += RemoveCredentials;
        _credentials.TryAdd(credentials.CredentialsId, credentials);

        return new CredentialsDto(credentials.CredentialsId);

        CredentialsAction? CheckIfExistsCredentials(Guid waiterId, out CredentialsAction credentialsAction) =>
            credentialsAction = _credentials.FirstOrDefault(x => x.Value.Waiter.Id.Equals(waiterId)).Value;
    }

    public bool CheckCredentials(Guid credentialsId, out Guid waiterId)
    {
        var returnValue = _credentials.TryGetValue(credentialsId, out var credentials);
        waiterId = credentials.Waiter.Id;
        return returnValue;
    }

    public void Dispose()
    {
        foreach (var credentials in _credentials.Values)
            credentials.TimerCallBackAction -= RemoveCredentials;
        GC.SuppressFinalize(this);
    }

    private void Credentials_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
            foreach (CredentialsAction newItem in e.NewItems)
                Log.Information($"{nameof(CredentialsCache)}. Added item: {JsonSerializer.Serialize(newItem, Options.JsonSerializerOptions)}");

        if (e.OldItems != null)
            foreach (CredentialsAction oldItem in e.OldItems)
                Log.Information($"{nameof(CredentialsCache)}. Remove item: {JsonSerializer.Serialize(oldItem, Options.JsonSerializerOptions)}");
    }

    private void RemoveCredentials(CredentialsAction credentials) =>
        _credentials.TryRemove(credentials.CredentialsId, out _);
}