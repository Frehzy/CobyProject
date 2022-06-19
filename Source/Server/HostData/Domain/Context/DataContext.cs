using HostData.Domain.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Shared.Data.Enum;

namespace HostData.Domain.Context;

public class DataContext : DbContext
{
    public DbSet<DiscountTypeEntity> AllDiscountTypes { get; set; }

    public DbSet<OrderEntity> AllOrders { get; set; }

    public DbSet<PaymentTypeEntity> AllPaymentTypes { get; set; }

    public DbSet<ProductItemEntity> AllProducts { get; set; }

    public DbSet<TableEntity> AllTables { get; set; }

    public DbSet<WaiterEntity> AllWaiters { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        Log.Information($"Database created status (True - Created, False - AlreadyExists): " +
                        $"{Database.EnsureCreated()}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var adminEntity = CreateAdminEntity();
        modelBuilder.Entity<WaiterEntity>().HasData(adminEntity);

        base.OnModelCreating(modelBuilder);

        static WaiterEntity CreateAdminEntity() =>
            new()
            {
                Id = new Guid("2c4c850c-728e-45a9-bb41-615f5723e0aa"),
                CreatedTime = DateTime.Now,
                IsDeleted = false,
                Name = "ADMIN",
                Password = "ADMINPASSWORD",
                Version = 1,
                WaiterCreatedId = Guid.Empty,
                Permissions = Enum.GetValues<EmployeePermission>().ToList()
            };
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();

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