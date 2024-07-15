using System.Reflection;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Queries;
using Confab.Shared.Infrastructure.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Queries;

public static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
        return services;
    }
}