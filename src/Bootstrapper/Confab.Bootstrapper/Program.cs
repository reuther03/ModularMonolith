using Confab.Bootstrapper;
using Confab.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var assemblies = ModuleLoader.LoadAssemblies();
var modules = ModuleLoader.LoadModules(assemblies);

services
    .AddInfrastructure();

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