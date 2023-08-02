using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Configuration
{
    public class PorudzbinaConfiguration : IEntityTypeConfiguration<Porudzbina>
    {
        public void Configure(EntityTypeBuilder<Porudzbina> builder)
        {
            builder.HasKey(p => p.IdPorudzbine);
            builder.Property(p => p.IdPorudzbine).ValueGeneratedOnAdd();
            builder.Property(p => p.Komentar).IsRequired();
            builder.Property(p => p.AdresaDostave).IsRequired().HasMaxLength(30);
            builder.Property(p => p.UkupnaCijena).IsRequired();
            builder.Property(p => p.VrijemeDostave).IsRequired();
            builder.Property(p => p.DatumPorudzbine).IsRequired();
            builder.Property(p => p.Status).IsRequired().HasConversion(new EnumToStringConverter<StatusPorudzbine>());



            builder.HasOne(p => p.Korisnik)
                .WithMany(k => k.Porudzbine)
                .HasForeignKey(p => p.KorisnikId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(p => p.PorudzbinaArtikal)
                .WithOne(pa => pa.Porudzbina)
                .HasForeignKey(s => s.IdPorudzbina)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
