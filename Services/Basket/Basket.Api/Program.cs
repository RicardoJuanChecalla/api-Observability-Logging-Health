using MassTransit;
using Basket.Api.Repositories;
using Discount.Grpc.Protos;
using Basket.Api.GrpcServices;
// using System.Security.Cryptography.X509Certificates;


var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration; 

// Add services to the container.
// Redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
    }
);

// General Configuration
builder.Services.AddScoped<IBasketRepository,BasketRepository>();
builder.Services.AddAutoMapper(typeof(Program));

// Grpc Configuration
//https://docs.microsoft.com/en-us/aspnet/core/grpc/clientfactory?view=aspnetcore-6.0
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
    (o => o.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]));
    //.ConfigurePrimaryHttpMessageHandler(() =>
    //{
    //    var cert = new X509Certificate2(configuration["GrpcSettings:CertificateFile"], configuration["GrpcSettings:CertificatePassword"]);
    //    var handler = new HttpClientHandler();
    //    handler.ClientCertificates.Add(cert);
    //    return handler;
    //});
builder.Services.AddScoped<DiscountGrpcService>();  

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config =>{
    config.UsingRabbitMq((context, cfg) =>  cfg.Host(configuration["EventBusSettings:HostAddress"]));
});
builder.Services.AddMassTransitHostedService();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
