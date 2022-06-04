namespace Api.Data.Guest;

public interface IGuest
{
    public Guid Id { get; }

    public string Name { get; }

    public int Rank { get; }
}