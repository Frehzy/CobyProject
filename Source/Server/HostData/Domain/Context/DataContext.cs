using HostData.Domain.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace HostData.Domain.Context;

public class DataContext : DbContext
{
    public DbSet<DiscountEntity> Discounts { get; set; }

    public DbSet<GuestEntity> Guests { get; set; }

    public DbSet<OrderEntity> Orders { get; set; }

    public DbSet<PaymentEntity> Payments { get; set; }

    public DbSet<PaymentTypeEntity> PaymentType { get; set; }

    public DbSet<PermissionEntity> Permissions { get;set; }

    public DbSet<ProductEntity> Products { get; set; }

    public DbSet<TableEntity> Tables { get; set; }

    public DbSet<WaiterEntity> Waiters { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
}