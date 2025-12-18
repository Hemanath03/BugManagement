

using Carter;
using MediatR;
using Microsoft.OpenApi;
using Modules.BugManagement;
using Serilog;
using Serilog.Events;
using Shared;
using UI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, lc) =>
{
    lc.MinimumLevel.Error()
      .WriteTo.Console()
      .WriteTo.File("Logs/log.txt", restrictedToMinimumLevel: LogEventLevel.Error);
});

builder.Services
    .AddBugManagementModule(builder.Configuration)
    .AddSharedServices();

builder.Services.AddCarter();


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Bug Management API",
        Version = "v1"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("http://localhost:5091/swagger/v1/swagger.json", "Bug Management API v1");
});


app.UseCors("AllowAll");

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapCarter();
app.Run();
