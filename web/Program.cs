global using web.Services;
global using web.Models;

using Microsoft.AspNetCore.SignalR;
using web.Components;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Logs;

var builder = WebApplication.CreateBuilder(args);

// Environment.GetEnvironmentVariable("SOME_ENVIRONMENT_VARIABLE");

var serviceName = "blazor-file-upload";
builder.Logging.AddOpenTelemetry(options =>
{
  options
    .SetResourceBuilder(
      ResourceBuilder
        .CreateDefault()
        .AddService(serviceName)
    )
    .AddOtlpExporter(o =>
    {
      o.Endpoint = new Uri(
        builder.Configuration["COLLECTOR_URL"] ?? 
        throw new NullReferenceException("environment variable not set: COLLECTOR_URL")
      );
    });
  // .AddConsoleExporter();
});
builder.Services.AddDbContext<MyDbContext>(config => {
    var myConnectionString = builder.Configuration["MYDATABASECONNECTIONSTRING"];
    // var myConnectionString = Environment.GetEnvironmentVariable("MYDATABASECONNECTIONSTRING");
    config.UseNpgsql(myConnectionString);
});

builder.Services.Configure<HubOptions>(options =>
{
    options.MaximumReceiveMessageSize = 1024 * 1024 * 200; // 200MB or use null
});

builder.Services.AddSingleton<UserService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

string i = null;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

public partial class Program { }
