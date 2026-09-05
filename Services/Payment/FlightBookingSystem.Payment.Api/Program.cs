using FlightBookingSystem.Payments.Application.Handlers;
using FlightBookingSystem.Payments.Core.Repositories;
using FlightBookingSystem.Payments.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using MassTransit;
using FlightBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using FlightBookingSystem.BuildingBlocks.Common;
using FlightBookingSystem.Payments.Application.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MediatR
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(ProcessPaymentHandler).Assembly,
    typeof(RefundPaymentHandler).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

//Add Services
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

//MassTransit
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<FlightBookedConsumer>();

    config.UsingRabbitMq((ct, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstant.FlightBookedQueue, c =>
        {
            c.ConfigureConsumer<FlightBookedConsumer>(ct);
        });
    });
});

//Add Sql Connection
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));


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
