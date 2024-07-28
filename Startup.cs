
namespace EmailSenderAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews(); 
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // the use of HTTPS
        app.UseHttpsRedirection();
        
        // Enable static file 
        app.UseStaticFiles();
        // Enable routing 
        app.UseRouting();
        // Enable authorization 
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            //Configure controller routing
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Email}/{action=Index}/{id?}");
        });
    }
}
