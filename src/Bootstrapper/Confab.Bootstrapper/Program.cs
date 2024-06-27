using Confab.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services
    .AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseInfrastructure();
app.MapControllers();

app.Run();