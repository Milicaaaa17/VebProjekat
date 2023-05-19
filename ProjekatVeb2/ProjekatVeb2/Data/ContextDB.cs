using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Data
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {
           
        }
        public DbSet<Kupac> Kupci { get; set; }
        public DbSet<Prodavac> Prodavci { get; set; }
        public DbSet<Administrator> Administratori { get; set; }
        public DbSet<Kupac> Artikli { get; set; }
        public DbSet<Kupac> Porudzbine { get; set; }
        public DbSet<PoruzdbinaArtikal> PoruzdbinaArtikli { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextDB).Assembly);
        }
    }
}
