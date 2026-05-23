var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomainServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

app.UseApiServices();

await app.RunAsync();
