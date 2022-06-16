namespace HostData.Mapper;

public interface IMapper
{
    TOut Map<TIn, TOut>(TIn source) where TIn : class, new() where TOut : class, new();

    IEnumerable<TOut> Map<TIn, TOut>(IEnumerable<TIn> source) where TIn : class, new() where TOut : class, new();
}