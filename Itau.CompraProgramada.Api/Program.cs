using Itau.CompraProgramada.Api.Infrastructure;
using Itau.CompraProgramada.Application.Contracts;
using Itau.CompraProgramada.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var conn = builder.Configuration.GetConnectionString("Default");
if (string.IsNullOrWhiteSpace(conn))
    throw new InvalidOperationException("Connection string 'Default' não encontrada. Confira appsettings.Development.json.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(conn);
});

builder.Services.AddScoped<ICestaRecomendacaoRepository, CestaRecomendacaoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
