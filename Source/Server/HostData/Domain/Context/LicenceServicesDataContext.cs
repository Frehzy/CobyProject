using HostData.Domain.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace HostData.Domain.Context;

public class LicenceServicesDataContext : DbContext
{
    public DbSet<LicenceEntity> AllLicences { get; set; }

    public LicenceServicesDataContext(DbContextOptions<LicenceServicesDataContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        Log.Information($"Database created status (True - Created, False - AlreadyExists): " +
                        $"{Database.EnsureCreated()}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var adminEntity = CreateDefaultLicences();
        modelBuilder.Entity<LicenceEntity>().HasData(adminEntity);

        base.OnModelCreating(modelBuilder);

        static LicenceEntity CreateDefaultLicences() =>
            new()
            {
                Id = new Guid("2f5264c6-a805-47ed-9052-b9dc461df8db"),
                CreatedTime = DateTime.Now,
                IsDeleted = false,
                OrganizationId = Guid.Empty,
                MaxReservedLicence = 10,
                ModuleLicenceId = 5050,
                WaiterCreatedId = Guid.Empty
            };
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

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