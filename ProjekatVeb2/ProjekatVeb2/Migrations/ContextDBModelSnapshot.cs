﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjekatVeb2.Data;

#nullable disable

namespace ProjekatVeb2.Migrations
{
    [DbContext(typeof(ContextDB))]
    partial class ContextDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProjekatVeb2.Models.Artikal", b =>
                {
                    b.Property<int>("IdArtikla")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdArtikla"));

                    b.Property<int>("Cijena")
                        .HasColumnType("int");

                    b.Property<int>("Kolicina")
                        .HasColumnType("int");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Slika")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("IdArtikla");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Artikli");
                });

            modelBuilder.Entity("ProjekatVeb2.Models.Korisnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatumRodjenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KorisnickoIme")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Lozinka")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PonoviLozinku")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Slika")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("StatusVerifikacije")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Verifikovan")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("KorisnickoIme")
                        .IsUnique();

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("ProjekatVeb2.Models.Porudzbina", b =>
                {
                    b.Property<int>("IdPorudzbine")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPorudzbine"));

                    b.Property<string>("AdresaDostave")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("CijenaDostave")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatumPorudzbine")
                        .HasColumnType("datetime2");

                    b.Property<string>("Komentar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UkupnaCijena")
                        .HasColumnType("int");

                    b.Property<DateTime>("VrijemeDostave")
                        .HasColumnType("datetime2");

                    b.HasKey("IdPorudzbine");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Porudzbine");
                });

            modelBuilder.Entity("ProjekatVeb2.Models.PoruzdbinaArtikal", b =>
                {
                    b.Property<int>("IdPorudzbina")
                        .HasColumnType("int");

                    b.Property<int>("ArtikalID")
                        .HasColumnType("int");

                    b.Property<int>("Kolicina")
                        .HasColumnType("int");

                    b.HasKey("IdPorudzbina", "ArtikalID");

                    b.HasIndex("ArtikalID");

                    b.ToTable("PoruzdbinaArtikli");
                });

            modelBuilder.Entity("ProjekatVeb2.Models.Artikal", b =>
                {
                    b.HasOne("ProjekatVeb2.Models.Korisnik", "Korisnik")
                        .WithMany("Artikili")
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("ProjekatVeb2.Models.Porudzbina", b =>
                {
                    b.HasOne("ProjekatVeb2.Models.Korisnik", "Korisnik")
                        .WithMany("Porudzbine")
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("ProjekatVeb2.Models.PoruzdbinaArtikal", b =>
                {
                    b.HasOne("ProjekatVeb2.Models.Artikal", "Artikal")
                        .WithMany("PoruceniArtikli")
                        .HasForeignKey("ArtikalID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjekatVeb2.Models.Porudzbina", "Porudzbina")
                        .WithMany("PorudzbinaArtikal")
                        .HasForeignKey("IdPorudzbina")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Artikal");

                    b.Navigation("Porudzbina");
                });

            modelBuilder.Entity("ProjekatVeb2.Models.Artikal", b =>
                {
                    b.Navigation("PoruceniArtikli");
                });

            modelBuilder.Entity("ProjekatVeb2.Models.Korisnik", b =>
                {
                    b.Navigation("Artikili");

                    b.Navigation("Porudzbine");
                });

            modelBuilder.Entity("ProjekatVeb2.Models.Porudzbina", b =>
                {
                    b.Navigation("PorudzbinaArtikal");
                });
#pragma warning restore 612, 618
        }
    }
}
