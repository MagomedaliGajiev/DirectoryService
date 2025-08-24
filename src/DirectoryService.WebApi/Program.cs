using System.Reflection;
using DirectoryService.Application.Locations.Interfaces;
using DirectoryService.Infrastructure;
using DirectoryService.Infrastructure.Repositories;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddScoped<DirectoryServiceDbContext>(_ =>
    new DirectoryServiceDbContext(builder.Configuration.GetConnectionString("DatabaseConnection")!));

builder.Services.AddScoped<ILocationRepository, LocationRepository>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.Load("DirectoryService.Application")));

builder.Services.AddValidatorsFromAssembly(Assembly.Load("DirectoryService.Application"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "DirectorySevice"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
