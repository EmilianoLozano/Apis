using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace apiFinal.Models
{
    public partial class ejemploProg3Context : DbContext
    {
        public ejemploProg3Context()
        {
        }

        public ejemploProg3Context(DbContextOptions<ejemploProg3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseNpgsql("User ID=prog3; Password=123456; Server=localhost; Database=ejemploProg3;Integrated Security=true; Pooling=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Spanish_Argentina.1252");

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.ToTable("personas");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.FechaNacimiento).HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.HasIndex(e => e.IdRol, "IX_usuarios_idRol");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Contrasenia).HasColumnName("contrasenia");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.FechaAlta).HasColumnName("fechaAlta");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.NombreUsu).HasColumnName("nombreUsu");

                // entity.HasOne(d => d.IdRolNavigation)
                //     .WithMany(p => p.Usuarios)
                //     .HasForeignKey(d => d.IdRol)
                //     .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
