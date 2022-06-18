using HostData.Domain.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Shared.Data.Enum;
using System.Linq;

namespace HostData.Domain.Context;

public class DataContext : DbContext
{
    public DbSet<DiscountEntity> AllDiscounts { get; set; }

    public DbSet<OrderEntity> AllOrders { get; set; }

    public DbSet<PaymentTypeEntity> AllPaymentTypes { get; set; }

    public DbSet<PaymentTypeEntity> AllPaymentType { get; set; }

    public DbSet<ProductEntity> AllProducts { get; set; }

    public DbSet<TableEntity> AllTables { get; set; }

    public DbSet<WaiterEntity> AllWaiters { get; set; }

    public DbSet<PermissionEntity> AllPermissions { get; set; }

    public DbSet<WaiterPermissionEntity> AllWaiterPermissions { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) 
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        Log.Information($"Database created status (True - Created, False - AlreadyExists): " +
                        $"{Database.EnsureCreated()}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var permissions = CreatePermissionEntity();
        var adminEntity = CreateAdminEntity();
        foreach (var permission in permissions)
            modelBuilder.Entity<PermissionEntity>().HasData(permission);

        modelBuilder.Entity<WaiterEntity>().HasData(adminEntity);

        modelBuilder.Entity<WaiterPermissionEntity>().HasData(CreateAdminPermissioEntity(adminEntity, permissions));

        base.OnModelCreating(modelBuilder);
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

    private IEnumerable<PermissionEntity> CreatePermissionEntity()
    {
        var result = new List<PermissionEntity>
        {
            CreatePermissionEntity("e256dce3-19e8-43f8-995f-5a7aa821c946", EmployeePermission.CanCreateOrder),
            CreatePermissionEntity("8a8ce162-257e-4f69-8aa9-b517404726e4", EmployeePermission.CanUpdateOrder),
            CreatePermissionEntity("7c2ea4b6-8cd0-4ee2-8d22-ac3932ea905b", EmployeePermission.CanCloseOrder),
            CreatePermissionEntity("f578dc7d-4344-4d90-985a-d4b9eaca3661", EmployeePermission.CanRemoveOrder),
            CreatePermissionEntity("666c73da-ba0e-4a74-88ad-7fb3154d6acc", EmployeePermission.CanAddGuestOnOrder),
            CreatePermissionEntity("55684f46-0dbb-48c9-b4b3-d927d53c7b3b", EmployeePermission.CanRemoveGuestOnOrder),
            CreatePermissionEntity("27aa8fa8-db9a-456c-9517-63966b832d78", EmployeePermission.CanAddPaymentOnOrder),
            CreatePermissionEntity("368558a3-e579-457c-b9fa-35618cfd34ea", EmployeePermission.CanRemovePaymentOnOrder),
            CreatePermissionEntity("058db51d-b54a-4b18-8c25-8ff7f12e0aa0", EmployeePermission.CanAddDishesOnOrder),
            CreatePermissionEntity("e5fdef70-3f4c-48ed-b042-953727a84caf", EmployeePermission.CanRemoveDishesOnOrder),
            CreatePermissionEntity("5b537529-6e0c-4127-8e15-6172745d94be", EmployeePermission.CanRemovePrintedDishesOnOrder),
            CreatePermissionEntity("a1460813-bfe1-4c7f-b658-62b5083f9e06", EmployeePermission.CanOpenCaseSession),
            CreatePermissionEntity("495f09fd-dfe2-45a0-b042-5731aede5c68", EmployeePermission.CanCloseCafeSession),
            CreatePermissionEntity("4921886e-0711-4179-bb86-dee55b8a6817", EmployeePermission.CanOpenOrdersOfOtherWaiters),
            CreatePermissionEntity("c689b455-0f8a-414b-8a69-64b165826792", EmployeePermission.CanClosePersonalShiftOfOtherWaiters),
            CreatePermissionEntity("a704fcde-ba5d-42be-89c8-f1e64ec8a522", EmployeePermission.CanAcceptPayment),
            CreatePermissionEntity("0188f550-96ed-418c-b330-1e7af3e550b6", EmployeePermission.CanSeeCaseSessionReport),
            CreatePermissionEntity("d90fb9f8-6e90-4de5-b239-23f45468931c", EmployeePermission.CanAddDiscountOnOrder),
            CreatePermissionEntity("a93b13ab-a858-4d96-889d-4bfffe2fbfbc", EmployeePermission.CanRemoveDiscountOnOrder),
            CreatePermissionEntity("31579291-81b3-4fa6-8e5b-e804695c00d6", EmployeePermission.CanSeeClosedOrders),
            CreatePermissionEntity("9a7c7a24-5660-4237-bb95-a34687c33c1b", EmployeePermission.CanChangeTableOnOrder),
            CreatePermissionEntity("17402627-055f-4a02-ac5b-b8f34e8898bb", EmployeePermission.CanChangeWaiterOnOrder)
        };

        return result;

        static PermissionEntity CreatePermissionEntity(string guid, EmployeePermission permission) =>
            new()
            {
                CreatedTime = DateTime.Now,
                EmployeePermission = permission,
                IsDeleted = false,
                Version = 1,
                WaiterCreatedId = Guid.Empty,
                Id = new Guid(guid)
            };
    }

    private WaiterEntity CreateAdminEntity() =>
        new()
        { 
            Id = new Guid("2c4c850c-728e-45a9-bb41-615f5723e0aa"),
            CreatedTime = DateTime.Now,
            IsDeleted = false,
            Name = "ADMIN",
            Password = "ADMINPASSWORD",
            Version = 1,
            WaiterCreatedId = Guid.Empty
        };

    private WaiterPermissionEntity CreateAdminPermissioEntity(WaiterEntity adminWaiter, IEnumerable<PermissionEntity> permissions) =>
        new()
        { 
            Id = adminWaiter.Id,
            CreatedTime = adminWaiter.CreatedTime,
            IsDeleted = adminWaiter.IsDeleted,
            Permissions = permissions.ToList(),
            Version = adminWaiter.Version,
            WaiterCreatedId = adminWaiter.WaiterCreatedId
        };
}