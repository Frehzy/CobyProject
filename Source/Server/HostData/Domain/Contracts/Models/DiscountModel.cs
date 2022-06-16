﻿namespace HostData.Domain.Contracts.Models;

public class DiscountModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal DiscountSum { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public DiscountModel() { }
}