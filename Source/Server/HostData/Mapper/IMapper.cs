namespace HostData.Mapper;

public interface IMapper
{
    TOut Map<TIn, TOut>(TIn source);

    IEnumerable<TOut> Map<TIn, TOut>(IEnumerable<TIn> source);
}