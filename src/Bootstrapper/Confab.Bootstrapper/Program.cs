using Confab.Bootstrapper;
using Confab.Shared.Infrastructure;
using Confab.Shared.Infrastructure.Modules;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.ConfigureModules();

services.AddEndpointsApiExplorer();

var assemblies = ModuleLoader.LoadAssemblies(configuration);
var modules = ModuleLoader.LoadModules(assemblies);

services.AddInfrastructure(assemblies, modules);

var serviceProvider = services.BuildServiceProvider();
using var scope = serviceProvider.CreateScope();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Modules loaded: {Modules}", modules.Select(x => x.Name));
foreach (var module in modules)
{
    module.Register(services);
}


var app = builder.Build();

app.UseInfrastructure();
foreach (var module in modules)
{
    module.Use(app);
}

app.MapModuleInfo();
app.MapControllers();

await app.RunAsync();