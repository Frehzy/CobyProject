using ASPHost.ConfigureServices;
using HostData.Domain.Context;
using HostData.Hubs;
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

            app.UseRouting();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

            app.UseOwin(x => x.UseNancy());

            app.UseEndpoints(endPoint =>
            {
                endPoint.MapHub<OrderHub>("/ordersNotification");
            });
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

            ConfigureRepository.ConfigureService(services);
            ConfigureServiceBase.ConfigureService(services);
            ConfigureMapper.ConfigureService(services);
            ConfigureController.ConfigureService(services);
            ConfigureCache.ConfigureService(services);

            services.AddSignalR();
        }
    }
}
