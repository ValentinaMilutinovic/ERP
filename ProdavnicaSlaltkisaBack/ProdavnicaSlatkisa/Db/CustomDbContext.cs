using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProdavnicaSlatkisa.API.Db;

public partial class CustomDbContext : DbContext
{
    public CustomDbContext()
    {
    }

    public CustomDbContext(DbContextOptions<CustomDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAdministrator> TblAdministrators { get; set; }

    public virtual DbSet<TblKorisnik> TblKorisniks { get; set; }

    public virtual DbSet<TblKorpa> TblKorpas { get; set; }

    public virtual DbSet<TblKupac> TblKupacs { get; set; }

    public virtual DbSet<TblProizvod> TblProzvodi { get; set; }

    public virtual DbSet<TblProizvodUkorpi> TblProizvodUkorpis { get; set; }

    public virtual DbSet<TblProizvodjac> TblProizvodjacs { get; set; }

    public virtual DbSet<TblRacun> TblRacuns { get; set; }

    public virtual DbSet<TblTipProizvodum> TblTipProizvoda { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        =>
		optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=ProdavnicaSlatkisa;Integrated Security=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAdministrator>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__tblAdmin__719FE4E8D4711B74");

            entity.ToTable("tblAdministrator");

            entity.Property(e => e.AdminId)
                .HasColumnName("AdminID");

            entity.Property(e => e.Jmbg)
                .HasMaxLength(13)
                .HasColumnName("JMBG");
            entity.Property(e => e.Lozinka).HasMaxLength(150);
            entity.Property(e => e.Username).HasMaxLength(500);

            entity.HasOne(d => d.Korisnik).WithOne(p => p.TblAdministrator)
                .HasForeignKey<TblAdministrator>(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Administrator_Korisnik");
        });

        modelBuilder.Entity<TblKorisnik>(entity =>
        {
            entity.HasKey(e => e.KorisnikId).HasName("PK__tblKoris__80B06D61899A89DC");

            entity.ToTable("tblKorisnik");

            entity.Property(e => e.KorisnikId)
                .HasColumnName("KorisnikID");
            entity.Property(e => e.Adresa).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Grad).HasMaxLength(30);
            entity.Property(e => e.Ime).HasMaxLength(50);
            entity.Property(e => e.Kontakt).HasMaxLength(30);
            entity.Property(e => e.Prezime).HasMaxLength(50);
        });

        modelBuilder.Entity<TblKorpa>(entity =>
        {
            entity.HasKey(e => e.KorpaId).HasName("PK__tblKorpa__C298DFB3144710A7");

            entity.ToTable("tblKorpa");

            entity.Property(e => e.KorpaId)
                .HasColumnName("KorpaID");
            entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");
            //entity.Property(e => e.RacunId).HasColumnName("RacunID");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.TblKorpas)
                .HasForeignKey(d => d.KorisnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Korpa_Korisnik");

           /* entity.HasOne(d => d.Racun).WithMany(p => p.TblKorpas)
                .HasForeignKey(d => d.RacunId)
                .HasConstraintName("FK_Korpa_Racun");*/
        });

        modelBuilder.Entity<TblKupac>(entity =>
        {
            entity.HasKey(e => e.KupacId).HasName("PK__tblKupac__A9593C7BFBB58D51");

            entity.ToTable("tblKupac");

            entity.Property(e => e.KupacId)
                .HasColumnName("KupacID");
            entity.Property(e => e.LozinkaRk).HasMaxLength(500);
            entity.Property(e => e.UsernameRk)
                .HasMaxLength(50)
                .HasColumnName("UsernameRK");

            entity.HasOne(d => d.Korisnik).WithOne(p => p.TblKupac)
                .HasForeignKey<TblKupac>(d => d.KupacId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kupac_Korisnik");
        });

        modelBuilder.Entity<TblProizvod>(entity =>
        {
            entity.HasKey(e => e.IdProizvoda).HasName("PK__tblProiz__B01F3D4D56DD1E77");

            entity.ToTable("tblProizvod");

            entity.Property(e => e.IdProizvoda)
                .HasColumnName("IDProizvod");
            entity.Property(e => e.Cena).HasColumnType("numeric(10, 2)");
            entity.Property(e => e.Idproizvodjaca).HasColumnName("IDProizvodjaca");
            entity.Property(e => e.TipProizvodaId).HasColumnName("TipProizvodaID");
            entity.Property(e => e.Naziv).HasColumnName("Naziv");

            entity.HasOne(d => d.IdproizvodjacaNavigation).WithMany(p => p.TblProizvod)
                .HasForeignKey(d => d.Idproizvodjaca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proizvod_Proizvodjac");

            entity.HasOne(d => d.TipProizvoda).WithMany(p => p.TblProizvodi)
                .HasForeignKey(d => d.TipProizvodaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proizvod_TipProizvoda");
        });

        modelBuilder.Entity<TblProizvodUkorpi>(entity =>
        {
            entity.HasKey(e => e.ProizUkorpiId).HasName("PK__tblProiz__8780F10959FA40CC");

            entity.ToTable("tblProizvodUKorpi", tb =>
            {
               
                tb.HasTrigger("brisanjeKorpeIVracanjeProizvodTriger");
                tb.HasTrigger("trg_AddProizvodToKorpa");
                
            });
            
            entity.Property(e => e.ProizUkorpiId)
                .HasColumnName("ProizUKorpiID");
            entity.Property(e => e.BrojKomada).HasColumnType("int");
            entity.Property(e => e.Iznos).HasColumnType("int");
            entity.Property(e => e.ProizvodId).HasColumnName("IDProizvod");
            entity.Property(e => e.KorpaId).HasColumnName("KorpaID");

            entity.HasOne(d => d.ProizvodNavigation).WithMany(p => p.TblProizvodUkorpis)
                .HasForeignKey(d => d.ProizvodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProizvodUKorpi_Proizvod");

            entity.HasOne(d => d.Korpa).WithMany(p => p.TblProizvodUkorpis)
                .HasForeignKey(d => d.KorpaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProizvodUKorpi_Korpa");
        });

        modelBuilder.Entity<TblProizvodjac>(entity =>
        {
            entity.HasKey(e => e.Idproizvodjaca).HasName("PK__tblProiz__4ABF484F14EEABBC");

            entity.ToTable("tblProizvodjac");

            entity.Property(e => e.Idproizvodjaca)
                .HasColumnName("IDProizvodjaca");
            entity.Property(e => e.NazivProizvodjaca).HasMaxLength(50);
            entity.Property(e => e.ZemljaPorekla).HasMaxLength(50);
        });

        modelBuilder.Entity<TblRacun>(entity =>
        {
            entity.HasKey(e => e.RacunId).HasName("PK__tblRacun__07B8F7ACCB50282D");

            entity.ToTable("tblRacun");

            entity.Property(e => e.RacunId)
                .HasColumnName("RacunID");
            entity.Property(e => e.DatumKupovine).HasColumnType("date");
            entity.Property(e => e.KorpaId).HasColumnName("KorpaID");
            entity.Property(e => e.ClientSecret).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.PaymentIntentId).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Status).HasMaxLength(100).IsUnicode(false);

            entity.HasOne(d => d.Korpa).WithMany(p => p.TblRacuns)
                .HasForeignKey(d => d.KorpaId)
                .HasConstraintName("FK_Racun_Korpa");
        });

        modelBuilder.Entity<TblTipProizvodum>(entity =>
        {
            entity.HasKey(e => e.TipProizvodaId).HasName("PK__tblTipPr__2A3E562CEE5424F0");

            entity.ToTable("tblTipProizvoda");

            entity.Property(e => e.TipProizvodaId)
                .HasColumnName("TipProizvodaID");
            entity.Property(e => e.Sastav).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
