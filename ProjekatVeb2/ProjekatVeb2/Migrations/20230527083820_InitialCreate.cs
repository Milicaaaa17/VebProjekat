using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjekatVeb2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    IdK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImePrezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerifikacijaKorisnika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.IdK);
                });

            migrationBuilder.CreateTable(
                name: "Artikli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cijena = table.Column<double>(type: "float", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IdKorisnik = table.Column<int>(type: "int", nullable: false),
                    Izbrisan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikli", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artikli_Korisnici_IdKorisnik",
                        column: x => x.IdKorisnik,
                        principalTable: "Korisnici",
                        principalColumn: "IdK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<double>(type: "float", nullable: false),
                    VerifikacijaPorudzbine = table.Column<int>(type: "int", nullable: false),
                    VrijemePorucivanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VrijemeDostave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdKorisnik = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Porudzbine_Korisnici_IdKorisnik",
                        column: x => x.IdKorisnik,
                        principalTable: "Korisnici",
                        principalColumn: "IdK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PoruzdbinaArtikli",
                columns: table => new
                {
                    IdPorudzbina = table.Column<int>(type: "int", nullable: false),
                    ArtikalID = table.Column<int>(type: "int", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoruzdbinaArtikli", x => new { x.IdPorudzbina, x.ArtikalID });
                    table.ForeignKey(
                        name: "FK_PoruzdbinaArtikli_Artikli_ArtikalID",
                        column: x => x.ArtikalID,
                        principalTable: "Artikli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoruzdbinaArtikli_Porudzbine_IdPorudzbina",
                        column: x => x.IdPorudzbina,
                        principalTable: "Porudzbine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artikli_IdKorisnik",
                table: "Artikli",
                column: "IdKorisnik");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_IdKorisnik",
                table: "Porudzbine",
                column: "IdKorisnik");

            migrationBuilder.CreateIndex(
                name: "IX_PoruzdbinaArtikli_ArtikalID",
                table: "PoruzdbinaArtikli",
                column: "ArtikalID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoruzdbinaArtikli");

            migrationBuilder.DropTable(
                name: "Artikli");

            migrationBuilder.DropTable(
                name: "Porudzbine");

            migrationBuilder.DropTable(
                name: "Korisnici");
        }
    }
}
