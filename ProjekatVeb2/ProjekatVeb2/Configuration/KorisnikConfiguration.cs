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
            builder.HasKey(k => k.IdK);
            builder.Property(k => k.IdK).ValueGeneratedOnAdd();
            builder.Property(k => k.KorisnickoIme).IsRequired();
            builder.Property(k => k.ImePrezime).IsRequired().HasMaxLength(50);
            builder.Property(k => k.Email).IsRequired();
            builder.Property(k => k.DatumRodjenja).IsRequired();
            builder.Property(k => k.Lozinka).IsRequired();
            builder.Property(k => k.Adresa).IsRequired().HasMaxLength(50);
            builder.Property(k => k.Tip).HasConversion(new EnumToStringConverter<TipKorisnika>());
            builder.Property(k => k.VerifikacijaKorisnika).HasConversion(new EnumToStringConverter<Verifikacija>());
        }
    }
}
