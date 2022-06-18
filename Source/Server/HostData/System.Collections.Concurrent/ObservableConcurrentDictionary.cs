using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.ComponentModel;

namespace HostData.System.Collections.Concurrent;

public sealed class ObservableConcurrentDictionary<TKey, TValue> : ConcurrentDictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged
{
    public ObservableConcurrentDictionary() : base() { }

    public ObservableConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection) { }

    public ObservableConcurrentDictionary(IEqualityComparer<TKey> comparer) : base(comparer) { }

    public ObservableConcurrentDictionary(int concurrencyLevel, int capacity) : base(concurrencyLevel, capacity)
    {

    }

    public ObservableConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
        : base(collection, comparer) { }

    public ObservableConcurrentDictionary(int concurrencyLevel, int capacity, IEqualityComparer<TKey> comparer)
        : base(concurrencyLevel, capacity, comparer) { }

    public ObservableConcurrentDictionary(int concurrencyLevel, IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
        : base(concurrencyLevel, collection, comparer) { }

    public new TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
    {
        TValue value;
        if (base.ContainsKey(key))
        {
            value = base.AddOrUpdate(key, addValueFactory, updateValueFactory);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value));
        }
        else
        {
            value = base.AddOrUpdate(key, addValueFactory, updateValueFactory);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
        }
        return value;
    }

    public new void Clear()
    {
        base.Clear();
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public new TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
    {
        TValue value;
        if (base.ContainsKey(key))
            value = base.GetOrAdd(key, valueFactory);
        else
        {
            value = base.GetOrAdd(key, valueFactory);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
        }
        return value;
    }

    public new TValue GetOrAdd(TKey key, TValue value)
    {
        if (base.ContainsKey(key))
            base.GetOrAdd(key, value);
        else
        {
            base.GetOrAdd(key, value);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
        }
        return value;
    }

    public new bool TryAdd(TKey key, TValue value)
    {
        bool tryAdd;
        if (tryAdd = base.TryAdd(key, value))
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
        return tryAdd;
    }

    public new bool TryRemove(TKey key, out TValue value)
    {
        bool tryRemove;
        if (tryRemove = base.TryRemove(key, out value))
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, value));
        return tryRemove;
    }

    public new bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue)
    {
        bool tryUpdate;
        if (tryUpdate = base.TryUpdate(key, newValue, comparisonValue))
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newValue));
        return tryUpdate;
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnCollectionChanged(NotifyCollectionChangedEventArgs e) =>
        CollectionChanged?.Invoke(this, e);
}
