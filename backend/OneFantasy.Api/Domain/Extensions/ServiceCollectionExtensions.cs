using Microsoft.Extensions.DependencyInjection;
using OneFantasy.Api.Domain.Abstractions;
using OneFantasy.Api.Domain.Implementations;

namespace OneFantasy.Api.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICompetitionService, CompetitionService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<ISeasonService, SeasonService>();
            services.AddScoped<ITeamService, TeamService>();
            return services;
        }

    }
}
