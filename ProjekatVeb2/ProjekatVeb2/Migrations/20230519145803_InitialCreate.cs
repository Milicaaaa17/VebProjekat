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
                name: "Administratori",
                columns: table => new
                {
                    KorisnickoIme = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ImePrezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administratori", x => x.KorisnickoIme);
                });

            migrationBuilder.CreateTable(
                name: "Kupac",
                columns: table => new
                {
                    KorisnickoIme = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusPorudzbine = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ImePrezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kupac", x => x.KorisnickoIme);
                    table.ForeignKey(
                        name: "FK_Kupac_Administratori_KorisnickoIme",
                        column: x => x.KorisnickoIme,
                        principalTable: "Administratori",
                        principalColumn: "KorisnickoIme");
                });

            migrationBuilder.CreateTable(
                name: "Prodavci",
                columns: table => new
                {
                    KorisnickoIme = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdministratorKorisnickoIme = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Verifikovan = table.Column<int>(type: "int", nullable: false),
                    StatusPorudzbine = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ImePrezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodavci", x => x.KorisnickoIme);
                    table.ForeignKey(
                        name: "FK_Prodavci_Administratori_AdministratorKorisnickoIme",
                        column: x => x.AdministratorKorisnickoIme,
                        principalTable: "Administratori",
                        principalColumn: "KorisnickoIme");
                });

            migrationBuilder.CreateTable(
                name: "Artikal",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<double>(type: "float", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProdavacID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artikal_Prodavci_ProdavacID",
                        column: x => x.ProdavacID,
                        principalTable: "Prodavci",
                        principalColumn: "KorisnickoIme",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtikalId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Kolicina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresaDostave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VrijemeDostave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VrijemePorudzbine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UkupnaCijena = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    KupacID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProdavacKorisnickoIme = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Porudzbina_Artikal_ArtikalId",
                        column: x => x.ArtikalId,
                        principalTable: "Artikal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Porudzbina_Kupac_KupacID",
                        column: x => x.KupacID,
                        principalTable: "Kupac",
                        principalColumn: "KorisnickoIme");
                    table.ForeignKey(
                        name: "FK_Porudzbina_Prodavci_ProdavacKorisnickoIme",
                        column: x => x.ProdavacKorisnickoIme,
                        principalTable: "Prodavci",
                        principalColumn: "KorisnickoIme");
                });

            migrationBuilder.CreateTable(
                name: "PoruzdbinaArtikli",
                columns: table => new
                {
                    IdPorudzbina = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ArtikalID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PorudzbinaId = table.Column<int>(type: "int", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    Cijena = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoruzdbinaArtikli", x => new { x.IdPorudzbina, x.ArtikalID });
                    table.ForeignKey(
                        name: "FK_PoruzdbinaArtikli_Artikal_ArtikalID",
                        column: x => x.ArtikalID,
                        principalTable: "Artikal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PoruzdbinaArtikli_Porudzbina_PorudzbinaId",
                        column: x => x.PorudzbinaId,
                        principalTable: "Porudzbina",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administratori_KorisnickoIme",
                table: "Administratori",
                column: "KorisnickoIme",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artikal_ProdavacID",
                table: "Artikal",
                column: "ProdavacID");

            migrationBuilder.CreateIndex(
                name: "IX_Kupac_KorisnickoIme",
                table: "Kupac",
                column: "KorisnickoIme",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbina_ArtikalId",
                table: "Porudzbina",
                column: "ArtikalId");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbina_KupacID",
                table: "Porudzbina",
                column: "KupacID");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbina_ProdavacKorisnickoIme",
                table: "Porudzbina",
                column: "ProdavacKorisnickoIme");

            migrationBuilder.CreateIndex(
                name: "IX_PoruzdbinaArtikli_ArtikalID",
                table: "PoruzdbinaArtikli",
                column: "ArtikalID");

            migrationBuilder.CreateIndex(
                name: "IX_PoruzdbinaArtikli_PorudzbinaId",
                table: "PoruzdbinaArtikli",
                column: "PorudzbinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Prodavci_AdministratorKorisnickoIme",
                table: "Prodavci",
                column: "AdministratorKorisnickoIme");

            migrationBuilder.CreateIndex(
                name: "IX_Prodavci_KorisnickoIme",
                table: "Prodavci",
                column: "KorisnickoIme",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoruzdbinaArtikli");

            migrationBuilder.DropTable(
                name: "Porudzbina");

            migrationBuilder.DropTable(
                name: "Artikal");

            migrationBuilder.DropTable(
                name: "Kupac");

            migrationBuilder.DropTable(
                name: "Prodavci");

            migrationBuilder.DropTable(
                name: "Administratori");
        }
    }
}
