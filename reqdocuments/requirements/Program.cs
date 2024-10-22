using Npgsql;
using MediatR;
using System.Data;
using requirements.Infrastructure.Data;
using requirements.Infrastructure.Data.Queries;
using requirements.Domain.Interfaces;
using requirements.Application;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<IDbConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("ConexionDB");
    return new NpgsqlConnection(connectionString);
});

// Registra MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Registra UsuariosService
builder.Services.AddScoped<UsuariosService>();

// Registra el repositorio
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();
app.Run();
