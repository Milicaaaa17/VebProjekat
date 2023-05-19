using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Configuration
{
    public class ArtikalConfiguration : IEntityTypeConfiguration<Artikal>
    {
        public void Configure(EntityTypeBuilder<Artikal> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Kolicina).IsRequired();
            builder.Property(p => p.Cijena).IsRequired();
            builder.Property(p => p.PoruceniArtikli).IsRequired();

            builder.HasOne(p => p.Prodavac).WithMany(p => p.Artikli).HasForeignKey(p => p.ProdavacID).IsRequired().OnDelete(DeleteBehavior.Cascade);


        }
    }
}
