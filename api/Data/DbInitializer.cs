using ProjetoIntegrador.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ProjetoIntegrador.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BancoContext context)
        {
            context.Database.EnsureCreated();

            // Verificar dados existem
            if (context.Usuarios.Any())
                return;
            
            var categorias = new Categoria[]
            {
                new Categoria { Descricao = "Química" },
                new Categoria { Descricao = "Biologia" },
                new Categoria { Descricao = "História do Brasil" },
                new Categoria { Descricao = "História Geral" },
                new Categoria { Descricao = "Anatomia" },
                new Categoria { Descricao = "Geografia" }
            };

            foreach (var c in categorias)
            {
                context.Categorias.Add(c);
            }

            context.SaveChanges();

            var palavras = new Palavra[]
            {
                new Palavra { },
            };
        }
    }
}