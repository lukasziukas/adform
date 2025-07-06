using Core;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
});
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddInfrastructure(connectionString)
    .AddCore();

builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new() { Title = "Adform API", Version = "v1" });
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
