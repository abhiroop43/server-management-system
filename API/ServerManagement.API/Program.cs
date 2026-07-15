using ServerManagement.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddDomainServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
    await app.InitializeDatabaseAsync();

await app.RunAsync();
