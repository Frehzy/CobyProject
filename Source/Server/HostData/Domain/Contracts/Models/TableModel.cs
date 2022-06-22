namespace HostData.Domain.Contracts.Models;

public class TableModel : BaseModel
{
    public int Number { get; set; }

    public string? Name { get; set; }

    public TableModel() : base() { }
}