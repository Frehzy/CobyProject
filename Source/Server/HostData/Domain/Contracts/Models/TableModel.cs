﻿namespace HostData.Domain.Contracts.Models;

public class TableModel : BaseModel
{
    public int Number { get; set; }

    public string? Name { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public bool IsDeleted { get; set; } = false;

    public TableModel() { }
}