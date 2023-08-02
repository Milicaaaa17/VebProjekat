using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Configuration
{
    public class ArtikalConfiguration : IEntityTypeConfiguration<Artikal>
    {
        public void Configure(EntityTypeBuilder<Artikal> builder)
        {
            builder.HasKey(a => a.IdArtikla);
            builder.Property(a => a.IdArtikla).ValueGeneratedOnAdd();
            builder.Property(a => a.Naziv).IsRequired();
            builder.Property(a => a.Cijena).IsRequired();
            builder.Property(a => a.Kolicina).IsRequired();
            builder.Property(a => a.Opis);
            builder.Property(a => a.Slika);

            builder.HasOne(p => p.Korisnik)
                .WithMany(k => k.Artikili)
                .HasForeignKey(p => p.KorisnikId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.PoruceniArtikli)
            .WithOne(pa => pa.Artikal)
            .HasForeignKey(pa => pa.ArtikalID)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
