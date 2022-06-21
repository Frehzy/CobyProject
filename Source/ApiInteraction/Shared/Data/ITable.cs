namespace Shared.Data;

public interface ITable
{
    public Guid Id { get; }

    public string Name { get; }

    public int Number { get; }
}