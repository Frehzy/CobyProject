namespace Shared.Data;

public interface IGuest
{
    public Guid Id { get; }

    public string Name { get; }

    public int Rank { get; }

    public bool IsDeleted { get; }
}