using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Configuration
{
    public class PorudzbinaConfiguration : IEntityTypeConfiguration<Porudzbina>
    {
        public void Configure(EntityTypeBuilder<Porudzbina> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Adresa).IsRequired().HasMaxLength(50);

            builder.HasOne(p => p.Korisnik).WithMany(p => p.Porudzbine).HasForeignKey(p => p.IdKorisnik).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
