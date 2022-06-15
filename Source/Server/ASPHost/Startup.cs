using HostData.Domain.Context;
using HostData.Domain.Contracts.Entities.Order;
using HostData.Domain.Contracts.Services;
using HostData.Domain.Repository;
using HostData.Mapper;
using HostData.Repository;
using HostData.Services;
using Microsoft.EntityFrameworkCore;
using Nancy.Owin;
using Shared.Data;

namespace ASPHost
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            app.UseOwin(x => x.UseNancy());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<DataContext>(options =>
            {
                options
                    .UseNpgsql(_configuration.GetConnectionString("DefaultConnection"),
                        assembly =>
                            assembly.MigrationsAssembly("ASPHost"));
            });

            services.AddScoped<IDbRepository, DbRepository>();

            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IGuestService, GuestService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IPaymentTypeService, PaymentTypeService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ITableService, TableService>();
            services.AddTransient<IWaiterService, WaiterService>();
            services.AddTransient<IConfigSettings, ConfigurationService>();

            services.AddTransient<IMapper, Mapper>();

            services.AddSingleton(new OrderWaiterEntity { Id = Guid.Empty });
        }
    }
}
