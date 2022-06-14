using HostData.Domain.Context;
using HostData.Domain.Contracts.Entities;
using HostData.Domain.Repository;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
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

            app.UseOwin(x => x.UseNancy());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options
                    .UseNpgsql(_configuration.GetConnectionString("DefaultConnection"),
                        assembly =>
                            assembly.MigrationsAssembly("ASPHost"));
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IDbRepository, DbRepository>();
            services.AddSingleton(new WaiterEntity { Id = Guid.Empty });
        }
    }
}
