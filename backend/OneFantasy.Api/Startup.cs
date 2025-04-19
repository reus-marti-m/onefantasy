using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OneFantasy.Api.Data;
using OneFantasy.Api.Domain.Extensions;

namespace OneFantasy.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            // DbContext config
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")
                )
            );

            // Domain services
            services.AddDomainServices();

            // Controllers
            services.AddControllers()
                .ConfigureApiBehaviorOptions(opts =>
                {
                    opts.InvalidModelStateResponseFactory = ctx =>
                    {
                        var pd = new ValidationProblemDetails(ctx.ModelState)
                        {
                            Title = "One or more validation errors occurred.",
                            Status = StatusCodes.Status400BadRequest,
                        };
                        return new BadRequestObjectResult(pd)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                });

            // 3. ASP.NET Core services
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OneFantasy API",
                    Version = "v1",
                    Description = "API per gestionar competicions i participacions"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Catch exceptions
            app.UseExceptionHandler("/error");

            // Dev settings
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OneFantasy API v1");
                        c.RoutePrefix = string.Empty;
                    }
                );
            }

            // Rest o f settings
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                }
            );
        }
    }
}