using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProdavnicaSlatkisa.API.Migrations
{
    /// <inheritdoc />
    public partial class Full : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblKorisnik",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Kontakt = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Grad = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblKoris__80B06D61899A89DC", x => x.KorisnikID);
                });

            migrationBuilder.CreateTable(
                name: "tblModel",
                columns: table => new
                {
                    IDModela = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivModela = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblModel__A33B9CC03447082F", x => x.IDModela);
                });

            migrationBuilder.CreateTable(
                name: "tblProizvodjac",
                columns: table => new
                {
                    IDProizvodjaca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivProizvodjaca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZemljaPorekla = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblProiz__4ABF484F14EEABBC", x => x.IDProizvodjaca);
                });

            migrationBuilder.CreateTable(
                name: "tblTipProizvoda",
                columns: table => new
                {
                    TipProizvodaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sastav = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblTipPr__2A3E562CEE5424F0", x => x.TipProizvodaID);
                });

            migrationBuilder.CreateTable(
                name: "tblAdministrator",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblAdmin__719FE4E8D4711B74", x => x.AdminID);
                    table.ForeignKey(
                        name: "FK_Administrator_Korisnik",
                        column: x => x.AdminID,
                        principalTable: "tblKorisnik",
                        principalColumn: "KorisnikID");
                });

            migrationBuilder.CreateTable(
                name: "tblKorpa",
                columns: table => new
                {
                    KorpaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikID = table.Column<int>(type: "int", nullable: false),
                    UkupanIznos = table.Column<int>(type: "int", nullable: true),
                    BrProizvoda = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblKorpa__C298DFB3144710A7", x => x.KorpaID);
                    table.ForeignKey(
                        name: "FK_Korpa_Korisnik",
                        column: x => x.KorisnikID,
                        principalTable: "tblKorisnik",
                        principalColumn: "KorisnikID");
                });

            migrationBuilder.CreateTable(
                name: "tblKupac",
                columns: table => new
                {
                    KupacID = table.Column<int>(type: "int", nullable: false),
                    UsernameRK = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LozinkaRk = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BrojKupovina = table.Column<int>(type: "int", nullable: true),
                    Registrovan = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblKupac__A9593C7BFBB58D51", x => x.KupacID);
                    table.ForeignKey(
                        name: "FK_Kupac_Korisnik",
                        column: x => x.KupacID,
                        principalTable: "tblKorisnik",
                        principalColumn: "KorisnikID");
                });

            migrationBuilder.CreateTable(
                name: "tblProizvod",
                columns: table => new
                {
                    IDProizvod = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Zapremina = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    IDProizvodjaca = table.Column<int>(type: "int", nullable: false),
                    IDModela = table.Column<int>(type: "int", nullable: false),
                    TipProizvodaID = table.Column<int>(type: "int", nullable: false),
                    KolicinaNaStanju = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblProiz__B01F3D4D56DD1E77", x => x.IDProizvod);
                    table.ForeignKey(
                        name: "FK_Proizvod_Model",
                        column: x => x.IDModela,
                        principalTable: "tblModel",
                        principalColumn: "IDModela");
                    table.ForeignKey(
                        name: "FK_Proizvod_Proizvodjac",
                        column: x => x.IDProizvodjaca,
                        principalTable: "tblProizvodjac",
                        principalColumn: "IDProizvodjaca");
                    table.ForeignKey(
                        name: "FK_Proizvod_TipProizvoda",
                        column: x => x.TipProizvodaID,
                        principalTable: "tblTipProizvoda",
                        principalColumn: "TipProizvodaID");
                });

            migrationBuilder.CreateTable(
                name: "tblRacun",
                columns: table => new
                {
                    RacunID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UkupanIznos = table.Column<int>(type: "int", nullable: false),
                    DatumKupovine = table.Column<DateTime>(type: "date", nullable: false),
                    VremeKupovine = table.Column<TimeSpan>(type: "time", nullable: false),
                    Popust = table.Column<bool>(type: "bit", nullable: true),
                    ProcenatPop = table.Column<int>(type: "int", nullable: true),
                    IznosSaPopustom = table.Column<int>(type: "int", nullable: true),
                    KorpaID = table.Column<int>(type: "int", nullable: false),
                    ClientSecret = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PaymentIntentId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblRacun__07B8F7ACCB50282D", x => x.RacunID);
                    table.ForeignKey(
                        name: "FK_Racun_Korpa",
                        column: x => x.KorpaID,
                        principalTable: "tblKorpa",
                        principalColumn: "KorpaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProizvodUKorpi",
                columns: table => new
                {
                    ProizUKorpiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojKomada = table.Column<int>(type: "int", nullable: false),
                    Iznos = table.Column<int>(type: "int", nullable: false),
                    IDProizvod = table.Column<int>(type: "int", nullable: false),
                    KorpaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblProiz__8780F10959FA40CC", x => x.ProizUKorpiID);
                    table.ForeignKey(
                        name: "FK_ProizvodUKorpi_Korpa",
                        column: x => x.KorpaID,
                        principalTable: "tblKorpa",
                        principalColumn: "KorpaID");
                    table.ForeignKey(
                        name: "FK_ProizvodUKorpi_Proizvod",
                        column: x => x.IDProizvod,
                        principalTable: "tblProizvod",
                        principalColumn: "IDProizvod");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblKorpa_KorisnikID",
                table: "tblKorpa",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_tblProizvod_IDModela",
                table: "tblProizvod",
                column: "IDModela");

            migrationBuilder.CreateIndex(
                name: "IX_tblProizvod_IDProizvodjaca",
                table: "tblProizvod",
                column: "IDProizvodjaca");

            migrationBuilder.CreateIndex(
                name: "IX_tblProizvod_TipProizvodaID",
                table: "tblProizvod",
                column: "TipProizvodaID");

            migrationBuilder.CreateIndex(
                name: "IX_tblProizvodUKorpi_IDProizvod",
                table: "tblProizvodUKorpi",
                column: "IDProizvod");

            migrationBuilder.CreateIndex(
                name: "IX_tblProizvodUKorpi_KorpaID",
                table: "tblProizvodUKorpi",
                column: "KorpaID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRacun_KorpaID",
                table: "tblRacun",
                column: "KorpaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAdministrator");

            migrationBuilder.DropTable(
                name: "tblKupac");

            migrationBuilder.DropTable(
                name: "tblProizvodUKorpi");

            migrationBuilder.DropTable(
                name: "tblRacun");

            migrationBuilder.DropTable(
                name: "tblProizvod");

            migrationBuilder.DropTable(
                name: "tblKorpa");

            migrationBuilder.DropTable(
                name: "tblModel");

            migrationBuilder.DropTable(
                name: "tblProizvodjac");

            migrationBuilder.DropTable(
                name: "tblTipProizvoda");

            migrationBuilder.DropTable(
                name: "tblKorisnik");
        }
    }
}
