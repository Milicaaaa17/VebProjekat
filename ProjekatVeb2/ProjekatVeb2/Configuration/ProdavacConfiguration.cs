using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Configuration
{
    public class ProdavacConfiguration : IEntityTypeConfiguration<Prodavac>

    {
        public void Configure(EntityTypeBuilder<Prodavac> builder)
        {
            builder.HasKey(a => a.KorisnickoIme);
            builder.Property(a => a.KorisnickoIme).IsRequired();
            builder.HasIndex(a => a.KorisnickoIme).IsUnique();
            builder.Property(a => a.ImePrezime).IsRequired();
            builder.Property(a => a.DatumRodjenja).IsRequired();
            builder.Property(a => a.Email).IsRequired();
            builder.Property(a => a.Adresa).IsRequired();
            builder.Property(a => a.Lozinka).IsRequired().HasMaxLength(20);


            builder.HasOne(s => s.Administrator).WithMany(s => s.Prodavci).IsRequired().OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(s => s.Artikli).WithOne(p => p.Prodavac).HasForeignKey(p => p.ProdavacID).IsRequired().OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
