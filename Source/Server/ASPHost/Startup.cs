using HostData.Cache.Credentials;
using HostData.Controller.Contract;
using HostData.Controller.Implementation;
using HostData.Domain.Context;
using HostData.Domain.Contracts.Services;
using HostData.Domain.Repository;
using HostData.Mapper;
using HostData.Repository;
using HostData.Services;
using Microsoft.EntityFrameworkCore;
using Nancy.Owin;

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
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

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

            services.AddTransient<IDiscountTypeService, DiscountTypeService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaymentTypeService, PaymentTypeService>();
            services.AddTransient<IProductItemService, ProductItemService>();
            services.AddTransient<ITableService, TableService>();
            services.AddTransient<IWaiterService, WaiterService>();

            services.AddTransient<IMapper, Mapper>();

            services.AddTransient<IWaiterController, WaiterController>();
            services.AddTransient<ICredentialsController, CredentialsController>();
            services.AddTransient<ITableController, TableController>();
            services.AddTransient<IPaymentTypeController, PaymentTypeController>();
            services.AddTransient<IDiscountTypeController, DiscountTypeController>();
            services.AddTransient<IProductItemController, ProductItemController>();
            services.AddTransient<IOrderController, OrderController>();
            services.AddTransient<ISessionController, SessionController>();

            services.AddTransient<ICredentialsCache, CredentialsCache>();
        }
    }
}
