using Itau.CompraProgramada.Api.Infrastructure;
using Itau.CompraProgramada.Application.Contracts;
using Itau.CompraProgramada.Application.UseCases.Cestas;
using Itau.CompraProgramada.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var conn = builder.Configuration.GetConnectionString("Default");
if (string.IsNullOrWhiteSpace(conn))
    throw new InvalidOperationException("Connection string 'Default' não encontrada. Confira appsettings.Development.json.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(conn);
});

builder.Services.AddScoped<CreateOrUpdateCestaTopFiveUseCase>();
builder.Services.AddScoped<GetCestaAtualUseCase>();
builder.Services.AddScoped<GetHistoricoCestasUseCase>();

builder.Services.AddScoped<ICestaRecomendacaoRepository, CestaRecomendacaoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
