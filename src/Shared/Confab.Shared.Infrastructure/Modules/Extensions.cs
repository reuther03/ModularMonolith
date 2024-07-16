using System.Reflection;
using Confab.Shared.Abstractions.Events;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Confab.Shared.Infrastructure.Modules;

public static class Extensions
{
    internal static IServiceCollection AddModuleInfo(this IServiceCollection services, IList<IModule> modules)
    {
        var moduleInfoProvider = new ModuleInfoProvider();
        var moduleInfos = modules.Select(x => new ModuleInfo(x.Name, x.Path, x.Policies));
        moduleInfoProvider.Modules.AddRange(moduleInfos);
        services.AddSingleton(moduleInfoProvider);

        return services;
    }

    internal static void MapModuleInfo(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("modules", context =>
        {
            var moduleInfoProvider = context.RequestServices.GetRequiredService<ModuleInfoProvider>();
            return context.Response.WriteAsJsonAsync(moduleInfoProvider.Modules);
        });
    }


    public static WebApplicationBuilder ConfigureModules(this WebApplicationBuilder builder)
    {
        var env = builder.Environment;
        ConfigureAppConfiguration(env, builder.Configuration);
        return builder;
    }

    private static void ConfigureAppConfiguration(IHostEnvironment env, IConfigurationBuilder config)
    {
        var settings = GetSettings("*", env);
        foreach (var setting in settings)
        {
            config.AddJsonFile(setting);
        }

        settings = GetSettings($"*.{env.EnvironmentName}", env);
        foreach (var setting in settings)
        {
            config.AddJsonFile(setting);
        }

        IEnumerable<string> GetSettings(string pattern, IHostEnvironment hostEnvironment)
            => Directory.EnumerateFiles(hostEnvironment.ContentRootPath, $"module.{pattern}.json", SearchOption.AllDirectories);
    }

    internal static IServiceCollection AddModuleRequests(this IServiceCollection services, IList<Assembly> assemblies)
    {
        services.AddModuleRegistry(assemblies);
        services.AddSingleton<IModuleClient, ModuleClient>();
        services.AddSingleton<IModuleSerializer, JsonModuleSerializer>();

        return services;
    }

    private static void AddModuleRegistry(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        var registry = new ModuleRegistry();

        var types = assemblies.SelectMany(x => x.GetTypes()).ToArray();
        var eventTypes = types
            .Where(x => x.IsClass && typeof(IEvent).IsAssignableFrom(x))
            .ToArray();

        services.AddSingleton<IModuleRegistry>(sp =>
        {
            var eventDispatcher = sp.GetRequiredService<IEventDispatcher>();
            var eventDispatcherType = eventDispatcher.GetType();

            foreach (var type in eventTypes)
            {
                registry.AddBroadcastAction(type, @event =>
                    (Task)eventDispatcherType.GetMethod(nameof(eventDispatcher.PublishAsync))
                        ?.MakeGenericMethod(type)
                        .Invoke(eventDispatcher, new[] { @event }));
            }

            return registry;
        });
    }
}