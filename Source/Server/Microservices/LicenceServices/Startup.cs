using Nancy.Owin;

namespace LicenceServices;

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
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
    }
}