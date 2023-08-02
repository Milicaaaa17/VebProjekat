using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Configuration
{
    public class KorisnikConfiguration : IEntityTypeConfiguration<Korisnik>
    {
        public void Configure(EntityTypeBuilder<Korisnik> builder)
        {
            builder.HasKey(k => k.IdKorisnika);
            builder.Property(k => k.IdKorisnika).ValueGeneratedOnAdd();
            builder.Property(k => k.KorisnickoIme).IsRequired();
            builder.HasIndex(k => k.KorisnickoIme).IsUnique();
            builder.Property(k => k.Tip).HasConversion(new EnumToStringConverter<TipKorisnika>());
            builder.Property(k => k.VerifikacijaKorisnika).HasConversion(new EnumToStringConverter<StatusVerifikacije>());

            builder.HasMany(k => k.Porudzbine)
           .WithOne(p => p.Korisnik)
           .HasForeignKey(p => p.KorisnikId);

            builder.HasMany(k => k.Artikili)
           .WithOne(a => a.Korisnik)
           .HasForeignKey(a => a.KorisnikId);
        }
    }
}
