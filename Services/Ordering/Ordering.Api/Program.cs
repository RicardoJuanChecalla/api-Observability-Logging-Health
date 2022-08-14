using Ordering.Infrastructure;
using Ordering.Application;
using Ordering.Api.Extensions;
using Ordering.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MigrateDatabase<OrderContext>((context,services)=>{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
        OrderContextSeed
            .SeedAsync(context,logger)
            .Wait();

});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json","Ordering.Api");
        options.RoutePrefix = string.Empty;
    } );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
