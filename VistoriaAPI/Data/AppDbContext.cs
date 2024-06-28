using Microsoft.EntityFrameworkCore;
using VistoriaAPI.Veiculos;

namespace VistoriaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Veiculo> Veiculos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=veiculos.db;")
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
    }
}