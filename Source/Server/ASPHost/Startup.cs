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

            app.UseEndpoints(endPoint =>
            {
                endPoint.MapHub<OrderHub>("/ordersNotification");
                endPoint.MapHub<WaiterHub>("/waitersNotification");
            });

            app.UseOwin(x => x.UseNancy());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            ConfigureRepository.ConfigureService(services);
            ConfigureServiceBase.ConfigureService(services);
            ConfigureMapper.ConfigureService(services);
            ConfigureController.ConfigureService(services);
            ConfigureCache.ConfigureService(services);
            ConfigureDatabase.ConfigureService(services, _configuration);

            services.AddSignalR(options => options.EnableDetailedErrors = true);
        }
    }
}
