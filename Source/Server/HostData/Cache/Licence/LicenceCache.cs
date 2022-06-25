using HostData.Cache.Entities;
using HostData.System.Collections.Concurrent;
using HostData.System.Text.Json;
using Serilog;
using System.Collections.Specialized;
using System.Text.Json;

namespace HostData.Cache.Licence;

public class LicenceCache : ILicenceCache
{
    private readonly ObservableConcurrentDictionary<int, LicenceEntity> _licences = new();
    private object _locker = new();

    public IReadOnlyList<LicenceEntity> Licences => _licences.Values.ToList();

    public LicenceCache()
    {
        _licences.CollectionChanged += Licences_CollectionChanged;
    }

    public bool AddLicence(int moduleLicenceId, string terminalId)
    {
        lock (_locker)
        {
            try
            {
                if (_licences.TryGetValue(moduleLicenceId, out var licenceOnCache) is true) //если такой модуль существует в кэше
                {
                    if (licenceOnCache.TerminalsId.All(x => x.Equals(terminalId)) is false) //и такой терминал не занял модуль
                        licenceOnCache.ReservedLicence(terminalId);
                }
                else
                {
                    var licenceEntity = new LicenceEntity();
                    licenceEntity.ReservedLicence(terminalId);
                    _licences.TryAdd(moduleLicenceId, licenceEntity);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public bool RemoveLicence(string terminalId)
    {
        lock (_locker)
        {
            try
            {
                var licences = _licences.Where(x => x.Value.TerminalsId.Any(x => x.Equals(terminalId))).Select(x => x.Value).ToList();
                foreach (var licence in licences)
                    licence.DisposeLicence(terminalId);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    private void Licences_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
            foreach (LicenceEntity newItem in e.NewItems)
                Log.Information($"{nameof(LicenceCache)}. Added item: {JsonSerializer.Serialize(newItem, Options.JsonSerializerOptions)}");

        if (e.OldItems != null)
            foreach (LicenceEntity oldItem in e.OldItems)
                Log.Information($"{nameof(LicenceCache)}. Remove item: {JsonSerializer.Serialize(oldItem, Options.JsonSerializerOptions)}");
    }
}