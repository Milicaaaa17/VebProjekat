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
        
        public DbSet<Korisnik> Korisnici { get; set; }   
        public DbSet<Artikal> Artikli { get; set; }
        public DbSet<Porudzbina> Porudzbine { get; set; }
        public DbSet<PoruzdbinaArtikal> PoruzdbinaArtikli { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextDB).Assembly);
        }
    }
}
