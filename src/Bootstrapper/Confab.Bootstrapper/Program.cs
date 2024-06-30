using Confab.Bootstrapper;
using Confab.Shared.Infrastructure;
using Confab.Shared.Infrastructure.Modules;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.ConfigureModules();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var assemblies = ModuleLoader.LoadAssemblies(configuration);
var modules = ModuleLoader.LoadModules(assemblies);

services
    .AddInfrastructure(assemblies, modules);

foreach (var module in modules)
{
    module.Register(services);
}


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseInfrastructure();
foreach (var module in modules)
{
    module.Use(app);
}

app.MapControllers();

app.Run();