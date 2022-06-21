namespace Shared.Data;

public interface ISession
{
    Guid Id { get; set; }

    int Version { get; }
}