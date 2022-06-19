namespace HostData.Domain.Contracts.Entities;

public class TableEntity : BaseEntity
{
    public int Number { get; set; }

    public string Name { get; set; }

    public TableEntity() { }
}