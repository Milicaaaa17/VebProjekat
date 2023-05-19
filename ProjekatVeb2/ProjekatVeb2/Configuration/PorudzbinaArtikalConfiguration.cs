using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Configuration
{
    public class PorudzbinaArtikalConfiguration : IEntityTypeConfiguration<PoruzdbinaArtikal>
    {
        public void Configure(EntityTypeBuilder<PoruzdbinaArtikal> builder)
        {
            builder.HasKey(pa => new { pa.IdPorudzbina, pa.ArtikalID });

            builder.HasOne(pa => pa.Porudzbina).WithMany(pa => pa.PoruceniArtikli).IsRequired().OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(pa => pa.Artikal).WithMany(pa => pa.PoruceniArtikli).IsRequired().OnDelete(DeleteBehavior.NoAction);
                
        }
    }
}
