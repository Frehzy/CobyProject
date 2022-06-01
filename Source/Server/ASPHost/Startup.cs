using ASPHost.Configuration;
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

            var appConfig = new AppConfiguration();
            ConfigurationBinder.Bind(_configuration, appConfig);

            app.UseOwin(x => x.UseNancy());
        }
    }
}
