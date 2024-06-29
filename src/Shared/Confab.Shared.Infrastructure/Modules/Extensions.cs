﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Confab.Shared.Infrastructure.Modules;

public static class Extensions
{
    // internal static IHostBuilder ConfigureModules(this IHostBuilder builder)
    //     => builder.ConfigureAppConfiguration((ctx, cfg) =>
    //     {
    //         foreach (var settings in GetSettings("*"))
    //         {
    //             cfg.AddJsonFile(settings);
    //         }
    //
    //         foreach (var settings in GetSettings($"*.{ctx.HostingEnvironment.EnvironmentName}"))
    //         {
    //             cfg.AddJsonFile(settings);
    //         }
    //
    //         IEnumerable<string> GetSettings(string pattern)
    //             => Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath, $"module.{pattern}.json", SearchOption.AllDirectories);
    //     });

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