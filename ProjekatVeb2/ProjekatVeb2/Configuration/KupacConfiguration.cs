using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Configuration
{
    public class KupacConfiguration : IEntityTypeConfiguration<Kupac>
    {
        public void Configure(EntityTypeBuilder<Kupac> builder)
        {
            builder.HasKey(a => a.KorisnickoIme);
            builder.Property(a => a.KorisnickoIme).IsRequired();
            builder.HasIndex(a => a.KorisnickoIme).IsUnique();
            builder.Property(a => a.ImePrezime).IsRequired();
            builder.Property(a => a.DatumRodjenja).IsRequired();
            builder.Property(a => a.Adresa).IsRequired();
            builder.Property(a => a.Email).IsRequired();
            builder.Property(a => a.Lozinka).IsRequired().HasMaxLength(20);

            builder.HasMany(k => k.Porudzbina).WithOne(o => o.Kupac).HasForeignKey(o => o.KupacID).IsRequired().OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(k => k.Administrator).WithMany(o => o.Kupci).HasForeignKey(o => o.KorisnickoIme).IsRequired().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
