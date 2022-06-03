using ASPHost.Configuration;
using HostData.Controllers.LogFactory;
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            var appConfig = new AppConfiguration();
            ConfigurationBinder.Bind(_configuration, appConfig);

            Log.LoggerFactory = loggerFactory;

            app.UseOwin(x => x.UseNancy());
        }
    }
}
