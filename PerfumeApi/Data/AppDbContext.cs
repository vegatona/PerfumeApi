using Microsoft.EntityFrameworkCore;
using PerfumeApi.Models;

namespace PerfumeApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }
        public DbSet<Perfume> Perfumes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Forzamos el mapeo por si los atributos del modelo no bastan
            modelBuilder.Entity<Perfume>().ToTable("perfumes");
            modelBuilder.Entity<MetodoPago>().ToTable("metodos_pago");
            modelBuilder.Entity<Usuario>().ToTable("usuarios");

            base.OnModelCreating(modelBuilder);
        }
    }
}