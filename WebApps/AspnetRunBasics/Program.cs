using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Common.Logging;

namespace AspnetRunBasics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(SeriLogger.Configure)
                // .UseSerilog((context, configuration)=>{
                //     configuration
                //         .Enrich.FromLogContext()
                //         .Enrich.WithMachineName()
                //         .WriteTo.Console()
                //         .WriteTo.Elasticsearch(
                //             new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"]))
                //             {
                //                 AutoRegisterTemplate = true,
                //                 NumberOfShards = 2,
                //                 NumberOfReplicas = 1,
                //                 IndexFormat = $"applogs-{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-logs-{DateTime.UtcNow:yyyy-MM}"
                //             })
                //         .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                //         .ReadFrom.Configuration(context.Configuration);
                // })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
