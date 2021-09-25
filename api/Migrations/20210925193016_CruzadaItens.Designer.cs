﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoIntegrador.Api.Models;

namespace api.Migrations
{
    [DbContext(typeof(BancoContext))]
    [Migration("20210925193016_CruzadaItens")]
    partial class CruzadaItens
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjetoIntegrador.Api.Models.Categoria", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("ProjetoIntegrador.Api.Models.Cruzada", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AutorID")
                        .HasColumnType("int");

                    b.Property<int?>("CategoriaID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Criacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("TamanhoX")
                        .HasColumnType("int");

                    b.Property<int>("TamanhoY")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AutorID");

                    b.HasIndex("CategoriaID");

                    b.ToTable("Cruzadas");
                });

            modelBuilder.Entity("ProjetoIntegrador.Api.Models.CruzadaItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CruzadaID")
                        .HasColumnType("int");

                    b.Property<int>("Orientacao")
                        .HasColumnType("int");

                    b.Property<int?>("PalavraID")
                        .HasColumnType("int");

                    b.Property<int>("PosicaoX")
                        .HasColumnType("int");

                    b.Property<int>("PosicaoY")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CruzadaID");

                    b.HasIndex("PalavraID");

                    b.ToTable("CruzadaItens");
                });

            modelBuilder.Entity("ProjetoIntegrador.Api.Models.Palavra", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoriaID")
                        .HasColumnType("int");

                    b.Property<string>("Dica")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Valor")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CategoriaID");

                    b.ToTable("Palavras");
                });

            modelBuilder.Entity("ProjetoIntegrador.Api.Models.Usuario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Perfil")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Usuarios");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Usuario");
                });

            modelBuilder.Entity("ProjetoIntegrador.Api.Models.Aluno", b =>
                {
                    b.HasBaseType("ProjetoIntegrador.Api.Models.Usuario");

                    b.Property<int?>("Idade")
                        .HasColumnType("int");

                    b.Property<int?>("SerieEscolar")
                        .HasColumnType("int");

                    b.Property<int?>("Sexo")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Aluno");
                });

            modelBuilder.Entity("ProjetoIntegrador.Api.Models.Professor", b =>
                {
                    b.HasBaseType("ProjetoIntegrador.Api.Models.Usuario");

                    b.Property<string>("Disciplina")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Professor");
                });

            modelBuilder.Entity("ProjetoIntegrador.Api.Models.Cruzada", b =>
                {
                    b.HasOne("ProjetoIntegrador.Api.Models.Usuario", "Autor")
                        .WithMany()
                        .HasForeignKey("AutorID");

                    b.HasOne("ProjetoIntegrador.Api.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaID");

                    b.Navigation("Autor");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("ProjetoIntegrador.Api.Models.CruzadaItem", b =>
                {
                    b.HasOne("ProjetoIntegrador.Api.Models.Cruzada", null)
                        .WithMany("Itens")
                        .HasForeignKey("CruzadaID");

                    b.HasOne("ProjetoIntegrador.Api.Models.Palavra", "Palavra")
                        .WithMany()
                        .HasForeignKey("PalavraID");

                    b.Navigation("Palavra");
                });

            modelBuilder.Entity("ProjetoIntegrador.Api.Models.Palavra", b =>
                {
                    b.HasOne("ProjetoIntegrador.Api.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaID");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("ProjetoIntegrador.Api.Models.Cruzada", b =>
                {
                    b.Navigation("Itens");
                });
#pragma warning restore 612, 618
        }
    }
}
