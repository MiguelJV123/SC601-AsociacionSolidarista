using Microsoft.AspNet.Identity.EntityFramework;
using proyecto.asociacionsolidarista.Models.Entities;
using proyecto.asociacionsolidarista.Models.Identity;
using System.Data.Entity;

namespace proyecto.asociacionsolidarista.Infrastructure.DbContexts
{
    public class AsociacionSolidaristaDbContext : IdentityDbContext<ApplicationUser>
    {
        public AsociacionSolidaristaDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Comercio> Comercios { get; set; }

        public static AsociacionSolidaristaDbContext Create()
        {
            return new AsociacionSolidaristaDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
        }
    }
}