using System.Linq.Expressions;

namespace SharedData.Mapper;

public sealed class Mapper : IMapper
{
    public TOut Map<TIn, TOut>(TIn source) where TIn : class where TOut : class
    {
        var typeIn = typeof(TIn);
        var typeOut = typeof(TOut);

        var outInstanceConstructor = typeOut.GetConstructor(Array.Empty<Type>());
        Func<TOut> _activator = outInstanceConstructor == null
            ? throw new KeyNotFoundException($"Default constructor for '{typeOut}' not found")
            : Expression.Lambda<Func<TOut>>(Expression.New(outInstanceConstructor)).Compile();

        var instanceOut = _activator();

        var propertiesIn = typeIn.GetProperties();
        var propertiesOut = typeOut.GetProperties().ToDictionary(x => x.Name);

        foreach (var propertyIn in propertiesIn)
        {
            if (propertiesOut.TryGetValue(propertyIn.Name, out var outProperty))
            {
                var sourceValue = propertyIn.GetValue(source);
                outProperty.SetValue(instanceOut, sourceValue);
            }
        }

        return instanceOut;
    }

    public IEnumerable<TOut> Map<TIn, TOut>(IEnumerable<TIn> source) where TIn : class where TOut : class
    {
        List<TOut> resultList = new();
        resultList.AddRange(from item in source
                            select Map<TIn, TOut>(item));
        return resultList;
    }
}