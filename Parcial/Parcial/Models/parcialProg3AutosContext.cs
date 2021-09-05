using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Parcial.Models
{
    public partial class parcialProg3AutosContext : DbContext
    {
        public parcialProg3AutosContext()
        {
        }

        public parcialProg3AutosContext(DbContextOptions<parcialProg3AutosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Marca> Marcas { get; set; }
        public virtual DbSet<Vehiculo> Vehiculos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseNpgsql("User ID=prog3Modelo2; Password=22Prog32021; Server=138.99.7.66; Database=parcialProg3Autos;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.ToTable("marcas");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Nombre).IsRequired();
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.ToTable("vehiculos");

                entity.HasIndex(e => e.IdMarca, "fki_fk_marca");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Color).IsRequired();

                entity.Property(e => e.Modelo).IsRequired();

                entity.HasOne(d => d.IdMarcaNavigation)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.IdMarca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_marca");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
