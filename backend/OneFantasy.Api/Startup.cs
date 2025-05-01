using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OneFantasy.Api.Data;
using OneFantasy.Api.Domain.Extensions;
using OneFantasy.Api.Models.Authentication;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace OneFantasy.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            // DbContext configuration
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")
                )
            );

            // Identity configuration
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Authentication with JWT (and Google/Facebook if enabled)
            var jwtSection = Configuration.GetSection("Jwt");
            var keyBytes = Encoding.UTF8.GetBytes(jwtSection["Key"]);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSection["Issuer"],
                    ValidAudience = jwtSection["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                };
            });
            //.AddGoogle(googleOpts =>
            //{
            //    var g = Configuration.GetSection("Authentication:Google");
            //    googleOpts.ClientId = g["ClientId"];
            //    googleOpts.ClientSecret = g["ClientSecret"];
            //})
            //.AddFacebook(fbOpts =>
            //{
            //    var f = Configuration.GetSection("Authentication:Facebook");
            //    fbOpts.AppId = f["AppId"];
            //    fbOpts.AppSecret = f["AppSecret"];
            //});

            // Authorization policies per role
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireUser", p => p.RequireRole("User", "Admin"));
                options.AddPolicy("RequireAdmin", p => p.RequireRole("Admin"));
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            // Register domain services
            services.AddDomainServices();

            // Automapper configuration (profiles)
            services.AddAutoMapper(typeof(Startup).Assembly);

            // Controllers with automatic validation responses
            services.AddControllers(opts =>
            {
#if DEBUG
                opts.Filters.Add(new Microsoft.AspNetCore.Mvc.Authorization.AllowAnonymousFilter());
#endif
            })
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

            // Swagger/OpenAPI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OneFantasy API",
                    Version = "v1",
                    Description = "API to manage competitions and participations"
                });

                // Define Bearer JWT scheme for Swagger UI
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Enter your JWT token (without the 'Bearer ' prefix).",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                // Require the Bearer scheme globally
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id   = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider svcProvider)
        {

            // Global exception handler
            app.UseExceptionHandler("/error");

            // Development settings
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OneFantasy API v1");
                        c.RoutePrefix = string.Empty;
                    }
                );
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                var controllerBuilder = endpoints.MapControllers();
                if (env.IsDevelopment())
                {
                    controllerBuilder.WithMetadata(new AllowAnonymousAttribute());
                }
            });

            // Seed roles at startup
            SeedRolesAsync(svcProvider).GetAwaiter().GetResult();
        }

        private static async Task SeedRolesAsync(IServiceProvider provider)
        {
            var roleMgr = provider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = ["Guest", "User", "Admin"];
            foreach (var r in roles)
            {
                if (!await roleMgr.RoleExistsAsync(r))
                {
                    await roleMgr.CreateAsync(new IdentityRole(r));
                }
            }
        }

    }
}