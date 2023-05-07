using MassTransit;
using Ordering.Infrastructure;
using Ordering.Application;
using Ordering.Api.Extensions;
using Ordering.Infrastructure.Persistence;
using Ordering.Api.EventBusConsumer;
using EventBus.Messages.Common;
using Serilog;
using Common.Logging;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration; 

// Add services to the container.
builder.Host.UseSerilog(SeriLogger.Configure);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config => {
    config.AddConsumer<BasketCheckoutConsumer>();
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, 
            c => c.ConfigureConsumer<BasketCheckoutConsumer>(ctx));
        cfg.UseHealthCheck(ctx);    
    });
});
builder.Services.AddMassTransitHostedService();

// General Configuration
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<BasketCheckoutConsumer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
                    .AddDbContextCheck<OrderContext>();

var app = builder.Build();

app.MigrateDatabase<OrderContext>((context,services)=>{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
        OrderContextSeed
            .SeedAsync(context,logger!)
            .Wait();

});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.UseSwaggerUI( options => {
    //     options.SwaggerEndpoint("/swagger/v1/swagger.json","Ordering.Api");
    //     options.RoutePrefix = string.Empty;
    // } );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

app.Run();
