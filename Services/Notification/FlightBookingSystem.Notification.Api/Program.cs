using FlightBookingSystem.BuildingBlocks.Common;
using FlightBookingSystem.Notifications.Application.Consumers;
using FlightBookingSystem.Notifications.Application.Handlers;
using FlightBookingSystem.Notifications.Application.Interface;
using FlightBookingSystem.Notifications.Application.Services;
using FlightBookingSystem.Notifications.Core.Repositories;
using FlightBookingSystem.Notifications.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assmeblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(SendNotificationHandler).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assmeblies));

//Add Services
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

//MassTransit
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<PaymentProcessedConsumer>();

    config.UsingRabbitMq((ct, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstant.PaymentProcessedQueue, c =>
        {
            c.ConfigureConsumer<PaymentProcessedConsumer>(ct);
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
