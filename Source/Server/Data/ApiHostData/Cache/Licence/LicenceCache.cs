using ApiHostData.Cache.Entities;
using Serilog;
using Shared.Factory.Dto;
using SharedData.System.Collections.Concurrent;
using SharedData.System.Text.Json;
using System.Collections.Specialized;
using System.Text.Json;

namespace ApiHostData.Cache.Licence;

public class LicenceCache : ILicenceCache
{
    private readonly ObservableConcurrentDictionary<int, LicenceAction> _licences = new();
    private readonly object _locker = new();

    public IReadOnlyList<LicenceAction> Licences => _licences.Values.ToList();

    public LicenceCache()
    {
        _licences.CollectionChanged += Licences_CollectionChanged;
    }

    public bool AddLicence(string terminalId, LicenceDto licence)
    {
        lock (_locker)
        {
            try
            {
                if (_licences.TryGetValue(licence.ModuleLicenceId, out var licenceOnCache) is true) //если такой модуль существует в кэше
                {
                    if (licenceOnCache.TerminalsId.All(x => x.Equals(terminalId)) is false) //и такой терминал не занял модуль
                        licenceOnCache.ReservedLicence(terminalId);
                }
                else
                {
                    //запрос в БД за количеством лицензий на организацию
                    var licenceEntity = new LicenceAction(licence.MaxReservedLicence);
                    licenceEntity.ReservedLicence(terminalId);
                    _licences.TryAdd(licence.ModuleLicenceId, licenceEntity);
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
            foreach (LicenceAction newItem in e.NewItems)
                Log.Information($"{nameof(LicenceCache)}. Added item: {JsonSerializer.Serialize(newItem, Options.JsonSerializerOptions)}");

        if (e.OldItems != null)
            foreach (LicenceAction oldItem in e.OldItems)
                Log.Information($"{nameof(LicenceCache)}. Remove item: {JsonSerializer.Serialize(oldItem, Options.JsonSerializerOptions)}");
    }
}