using Microsoft.AspNet.Identity.EntityFramework;
using proyecto.asociacionsolidarista.Models.Entities;
using proyecto.asociacionsolidarista.Models.Identity;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;


namespace proyecto.asociacionsolidarista.Infrastructure.DbContexts
{
    public class AsociacionSolidaristaDbContext : IdentityDbContext<ApplicationUser>
    {
        public AsociacionSolidaristaDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Comercio> Comercios { get; set; }

        public DbSet<CajaSINPE> CajasSINPE { get; set; }

        public DbSet<PagoSINPE> PagosSINPE { get; set; }

        public DbSet<BitacoraEventos> BitacoraEventos { get; set; }

        public static AsociacionSolidaristaDbContext Create()
        {
            return new AsociacionSolidaristaDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //-----------------------------------------
            // Comercio
            //-----------------------------------------

            modelBuilder.Entity<Comercio>()
                .ToTable("Comercio")
                .HasKey(c => c.IdComercio);

            modelBuilder.Entity<Comercio>()
                .Property(c => c.Identificacion)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<Comercio>()
                .Property(c => c.TipoIdentificacion)
                .IsRequired();

            modelBuilder.Entity<Comercio>()
                .Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Comercio>()
                .Property(c => c.Telefono)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Comercio>()
                .Property(c => c.CorreoElectronico)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Comercio>()
                .Property(c => c.Direccion)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<Comercio>()
                .Property(c => c.FechaDeRegistro)
                .IsRequired();

            modelBuilder.Entity<Comercio>()
                .Property(c => c.Estado)
                .IsRequired();

            //-----------------------------------------
            // CajaSINPE
            //-----------------------------------------

            modelBuilder.Entity<CajaSINPE>()
                .ToTable("CajaSINPE")
                .HasKey(c => c.IdCaja);

            modelBuilder.Entity<CajaSINPE>()
                .Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<CajaSINPE>()
                .Property(c => c.Descripcion)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<CajaSINPE>()
                .Property(c => c.TelefonoSINPE)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<CajaSINPE>()
                .Property(c => c.FechaDeRegistro)
                .IsRequired();

            modelBuilder.Entity<CajaSINPE>()
                .Property(c => c.Estado)
                .IsRequired();

            modelBuilder.Entity<CajaSINPE>()
                .HasRequired(c => c.Comercio)
                .WithMany(c => c.CajasSINPE)
                .HasForeignKey(c => c.IdComercio);

            //-----------------------------------------
            // PagoSINPE
            //-----------------------------------------

            modelBuilder.Entity<PagoSINPE>()
                .ToTable("PagoSINPE")
                .HasKey(p => p.IdSinpe);

            modelBuilder.Entity<PagoSINPE>()
                .Property(p => p.TelefonoOrigen)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<PagoSINPE>()
                .Property(p => p.NombreOrigen)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<PagoSINPE>()
                .Property(p => p.TelefonoDestinatario)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<PagoSINPE>()
                .Property(p => p.NombreDestinatario)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<PagoSINPE>()
                .Property(p => p.Monto)
                .IsRequired()
                .HasPrecision(18, 2);

            modelBuilder.Entity<PagoSINPE>()
                .Property(p => p.Descripcion)
                .HasMaxLength(50);

            modelBuilder.Entity<PagoSINPE>()
                .Property(p => p.FechaDeRegistro)
                .IsRequired();

            modelBuilder.Entity<PagoSINPE>()
                .Property(p => p.Estado)
                .IsRequired();

            modelBuilder.Entity<PagoSINPE>()
                .HasRequired(p => p.CajaSINPE)
                .WithMany(c => c.PagosSINPE)
                .HasForeignKey(p => p.IdCaja);

            //-----------------------------------------
            // BitacoraEventos
            //-----------------------------------------

            modelBuilder.Entity<BitacoraEventos>()
                .ToTable("BitacoraEventos")
                .HasKey(b => b.IdEvento);

            modelBuilder.Entity<BitacoraEventos>()
                .Property(b => b.TablaDeEvento)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<BitacoraEventos>()
                .Property(b => b.TipoDeEvento)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<BitacoraEventos>()
                .Property(b => b.FechaDeEvento)
                .IsRequired();

            modelBuilder.Entity<BitacoraEventos>()
                .Property(b => b.DescripcionDeEvento)
                .IsRequired();

            modelBuilder.Entity<BitacoraEventos>()
                .Property(b => b.DatosAnteriores);

            modelBuilder.Entity<BitacoraEventos>()
                .Property(b => b.DatosPosteriores);

            modelBuilder.Entity<BitacoraEventos>()
                .HasOptional(b => b.PagoSINPE)
                .WithMany(p => p.BitacoraEventos)
                .HasForeignKey(b => b.IdSinpe);
        }
    }
}