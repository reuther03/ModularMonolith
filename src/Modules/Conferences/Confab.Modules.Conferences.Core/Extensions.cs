using System.Runtime.CompilerServices;
using Confab.Modules.Conferences.Core.DAL;
using Confab.Modules.Conferences.Core.DAL.Repositories;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Modules.Conferences.Core.Services;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo(assemblyName: "Confab.Modules.Conferences.Api")]

namespace Confab.Modules.Conferences.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<ConferencesDbContext>();

        // services.AddSingleton<IHostRepository, HostInMemoryRepository>();
        services.AddScoped<IHostRepository, HostRepository>();
        services.AddSingleton<IHostDeletionPolicy, HostDeletionPolicy>();
        services.AddScoped<IHostService, HostService>();

        // services.AddSingleton<IConferenceRepository, ConferenceInMemoryRepository>();
        services.AddSingleton<IConferenceDeletionPolicy, ConferenceDeletionPolicy>();
        services.AddScoped<IConferenceRepository, ConferenceRepository>();
        services.AddScoped<IConferenceService, ConferenceService>();

        return services;
    }
}