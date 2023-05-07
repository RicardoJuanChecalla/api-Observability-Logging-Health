using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using Serilog;
using Common.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) => 
    config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true) );

builder.Host.UseSerilog(SeriLogger.Configure);
// builder.Host.ConfigureLogging((hostingContext, loggingbuilder) =>
//                 {
//                     loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
//                     loggingbuilder.AddConsole();
//                     loggingbuilder.AddDebug();
//                 });

builder.Services.AddOcelot()
    .AddCacheManager(settings => settings.WithDictionaryHandle());

var app = builder.Build();

app.UseOcelot().Wait();

app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
