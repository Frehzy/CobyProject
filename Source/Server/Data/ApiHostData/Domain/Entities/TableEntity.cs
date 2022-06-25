using SharedData.Entities.Implementation;

namespace ApiHostData.Domain.Entities;

public class TableEntity : BaseEntity
{
    public int Number { get; set; }

    public string Name { get; set; }

    public TableEntity() { }
}