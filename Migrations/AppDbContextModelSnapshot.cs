﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using crudmysql.Models;

namespace CrudCore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("crudmysql.Models.Funcionalidade", b =>
                {
                    b.Property<int>("IdFuncionalidade")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<int>("IdPerfil");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("IdFuncionalidade");

                    b.HasIndex("IdPerfil");

                    b.ToTable("Funcionalidades");
                });

            modelBuilder.Entity("crudmysql.Models.Perfil", b =>
                {
                    b.Property<int>("IdPerfil")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("IdPerfil");

                    b.ToTable("Perfis");
                });

            modelBuilder.Entity("crudmysql.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdPerfil");

                    b.Property<int>("Idade");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("IdPerfil");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("crudmysql.Models.Funcionalidade", b =>
                {
                    b.HasOne("crudmysql.Models.Perfil", "Perfil")
                        .WithMany("Funcionalidades")
                        .HasForeignKey("IdPerfil")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("crudmysql.Models.Usuario", b =>
                {
                    b.HasOne("crudmysql.Models.Perfil", "Perfil")
                        .WithMany()
                        .HasForeignKey("IdPerfil")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
