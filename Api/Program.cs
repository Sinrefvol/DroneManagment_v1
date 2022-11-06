using Infrastructure;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using System.Runtime.CompilerServices;
using Domain.AggregatesModel.DroneAggregate;
using Infrastructure.Repository;
using FluentValidation;
using System;
using API.Application.Commands;
using API.Application.Contracts;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IValidator<AddDroneContract>, AddDroneValidator>();
builder.Services.AddDbContext<DroneContext>(options =>
    {
        options.UseSqlServer(configuration.GetConnectionString(nameof(DroneContext)), b => b.MigrationsAssembly("API"));
    });

builder.Services.AddScoped<IDroneRepository, DroneRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();


app.MapControllers();

//Run migrations on startup if there are any

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DroneContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();
public partial class Program { }