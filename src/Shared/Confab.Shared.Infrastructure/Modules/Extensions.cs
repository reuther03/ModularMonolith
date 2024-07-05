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
        var moduleInfos = modules.Select(x => new ModuleInfo(x.Name, x.Path, x.Policies ?? Enumerable.Empty<string>())) ?? Enumerable.Empty<ModuleInfo>();
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
}