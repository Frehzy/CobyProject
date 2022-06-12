using Shared.Data;

namespace Shared.Factory.InternalModel;

internal class Table : ITable
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Number { get; set; }

    public bool IsDeleted { get; set; }

    public Table() { }

    public Table(Guid id, string name, int number, bool isDeleted)
    {
        Id = id;
        Name = name;
        Number = number;
        IsDeleted = isDeleted;
    }
}