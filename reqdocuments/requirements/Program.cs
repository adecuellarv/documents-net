using Npgsql;
using MediatR;
using System.Data;
using requirements.Infrastructure.Data;
using requirements.Infrastructure.Data.Queries;
using requirements.Domain.Interfaces;
using requirements.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddScoped<SolicitantesServicio>();
builder.Services.AddScoped<RequisitosServicio>();
builder.Services.AddScoped<DocumentosServicio>();

// Registra el repositorio
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<ISolicitantesRepository, SolicitantesRepository>();
builder.Services.AddScoped<IRequisitosRepository, RequisitosRepository>();
builder.Services.AddScoped<IDocumentosRepository, DocumentosRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Configurar servicios
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5173",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Agregar servicios de controladores
builder.Services.AddControllers();

var app = builder.Build();

// Configurar el middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Usar CORS
app.UseCors("AllowLocalhost5173");
app.UseRouting();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
