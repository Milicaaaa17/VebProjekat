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
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Naziv).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Kolicina).IsRequired();
            builder.Property(p => p.Cijena).IsRequired();
           

            builder.HasOne(p => p.Korisnik).WithMany(p => p.Artikili).HasForeignKey(p => p.IdKorisnik).IsRequired().OnDelete(DeleteBehavior.Restrict);


        }
    }
}
