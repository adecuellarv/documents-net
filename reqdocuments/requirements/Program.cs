using Npgsql;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IDbConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("ConexionDB");
    return new NpgsqlConnection(connectionString);
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
