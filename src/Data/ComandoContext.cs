using Microsoft.EntityFrameworkCore;
using CommandsApi.Models;

namespace CommandsApi.Data
{
    public class ComandoContext : DbContext
    {
        public ComandoContext(DbContextOptions<ComandoContext> options) : base(options) { }
        public DbSet<Comando> MisComandos { get; set; }
    }
}
