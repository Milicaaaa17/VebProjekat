using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Configuration
{
    public class AdministratorConfiguration : IEntityTypeConfiguration<Administrator>
    {
        public void Configure(EntityTypeBuilder<Administrator> builder)
        {
            builder.HasKey(a => a.KorisnickoIme);
            builder.Property(a => a.KorisnickoIme).IsRequired();
            builder.HasIndex(a => a.KorisnickoIme).IsUnique();
            builder.Property(a => a.ImePrezime).IsRequired();
            builder.Property(a => a.DatumRodjenja).IsRequired();
            builder.Property(a => a.Adresa).IsRequired();
            builder.Property(a => a.Email).IsRequired();
            builder.Property(a => a.Lozinka).IsRequired().HasMaxLength(20);
        }
    }
}
