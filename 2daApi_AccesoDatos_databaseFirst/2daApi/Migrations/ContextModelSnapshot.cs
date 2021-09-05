﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace _2daApi.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Models.Persona", b =>
                {
                    b.Property<int>("IdPersona")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Apellido")
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<bool>("Soltero")
                        .HasColumnType("boolean");

                    b.Property<int>("edad")
                        .HasColumnType("integer");

                    b.HasKey("IdPersona");

                    b.ToTable("personas");
                });

            modelBuilder.Entity("Models.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("ModelsUsuario.Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ApellidoUsu")
                        .HasColumnType("text");

                    b.Property<bool>("activo")
                        .HasColumnType("boolean");

                    b.Property<string>("contrasenia")
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .HasColumnType("text");

                    b.Property<DateTime>("fechaAlta")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("idRol")
                        .HasColumnType("integer");

                    b.Property<string>("nombreUsu")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("idRol");

                    b.ToTable("usuarios");
                });

            modelBuilder.Entity("ModelsUsuario.Usuario", b =>
                {
                    b.HasOne("Models.Rol", "rol")
                        .WithMany()
                        .HasForeignKey("idRol")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("rol");
                });
#pragma warning restore 612, 618
        }
    }
}
