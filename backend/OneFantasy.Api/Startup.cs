using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneFantasy.Api.Data;   // <- el teu namespace de l’AppDbContext

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        // Afegeix aquest bloc:
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

        // La resta de serveis ja existents:
        services.AddControllers();
        // ... qualsevol altra cosa que tinguis
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // ... el teu pipeline (UseRouting, UseEndpoints, etc.)
    }
}