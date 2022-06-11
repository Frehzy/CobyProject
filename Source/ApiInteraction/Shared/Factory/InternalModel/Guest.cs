using Shared.Data;

namespace Shared.Factory.InternalModel;

internal class Guest : IGuest
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Rank { get; set; }

    public bool IsDeleted { get; set; }

    public Guest() { }

    public Guest(Guid id, string name, int rank, bool isDeleted)
    {
        Id = id;
        Name = name;
        Rank = rank;
        IsDeleted = isDeleted;
    }
}