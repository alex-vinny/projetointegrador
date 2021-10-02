using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Api.Models;

namespace ProjetoIntegrador.Api.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Palavra> Palavras { get; set; }
        public DbSet<Cruzada> Cruzadas { get; set; }
        public DbSet<CruzadaItem> CruzadaItens { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>()
                .HasIndex(p => p.DescricaoSemAcento)
                .IsUnique(true);

             modelBuilder.Entity<Palavra>()
                .HasIndex(p => new { p.ValorSemAcento, p.DicaSemAcento })
                .IsUnique(true);
            
            modelBuilder
                .Entity<Usuario>()
                .HasIndex(p => new { p.Email, p.Nome })
                .IsUnique(true);
        }
    }
}