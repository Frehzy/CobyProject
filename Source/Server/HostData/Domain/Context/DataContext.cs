using HostData.Domain.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HostData.Domain.Context;

public class DataContext : DbContext
{
    public DbSet<DiscountEntity> AllDiscounts { get; set; }

    public DbSet<OrderEntity> AllOrders { get; set; }

    public DbSet<PaymentTypeEntity> AllPaymentTypes { get; set; }

    public DbSet<PaymentTypeEntity> AllPaymentType { get; set; }

    public DbSet<PermissionEntity> AllPermissions { get; set; }

    public DbSet<ProductEntity> AllProducts { get; set; }

    public DbSet<TableEntity> AllTables { get; set; }

    public DbSet<WaiterEntity> AllWaiters { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured is false)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }
}