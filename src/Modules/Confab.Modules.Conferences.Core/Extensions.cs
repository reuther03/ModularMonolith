using System.Runtime.CompilerServices;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo(assemblyName: "Confab.Modules.Conferences.Api")]
namespace Confab.Modules.Conferences.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IHostRepository, HostRepository>();
        services.AddSingleton<IHostDeletionPolicy, HostDeletionPolicy>();
        services.AddSingleton<IConferenceDeletionPolicy, ConferenceDeletionPolicy>();

        services.AddScoped<IHostService, HostService>();

        return services;
    }
}