using Data;
using Domain.Ports;
using Application.Ports;
using Microsoft.EntityFrameworkCore;
using Data.Guest;
using Application.Guest;
using Application.Room.Ports;
using Data.Room;
using Data.Booking;
using Application.Room;
using Application.Booking;
using Application.Booking.Ports;
using Application.Payment.Ports;
using System.Text.Json.Serialization;
using Payments.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region IoC
builder.Services.AddScoped<IGuestManager, GuestManager>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomManager, RoomManager>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingManager, BookingManager>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IPaymentProcessorFactory, PaymentProcessorFactory>();
#endregion

#region BR wiring up
var connectionString = builder.Configuration.GetConnectionString("Main");
object value = builder.Services.AddDbContext<HotelDbContext>(options => options.UseSqlServer(connectionString));
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

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
