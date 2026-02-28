using Microsoft.EntityFrameworkCore;

namespace Itau.CompraProgramada.Api.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

}