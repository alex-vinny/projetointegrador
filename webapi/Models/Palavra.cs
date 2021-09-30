using ProjetoIntegrador.Api.Dtos;
using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Models
{
    public class Palavra
    {
        public int ID { get; set; }
        public string Valor { get; set; }
        public string Dica { get; set; }
        public int? SerieEscolar { get; set; }
        public Categoria Categoria { get; set; }

        public PalavraDto ToDto()
        {
            return new PalavraDto
            {
                Id = ID,
                CategoriaId = Categoria?.ID,
                Categoria = Categoria?.Descricao,
                Palavra = Valor,
                DicaPalavra = Dica,
                SerieEscolar = SerieEscolar
            };
        }

        public Palavra UpdateFrom(PalavraDto palavra)
        {
            Valor = palavra.Palavra;
            Dica = palavra.DicaPalavra;
            SerieEscolar = palavra.SerieEscolar;
            
            if (palavra.CategoriaId.HasValue &&
                !Categoria.ID.Equals(palavra.CategoriaId.Value))
            {   
                Categoria = new Categoria
                {
                    ID = palavra.CategoriaId.Value
                };

                if (Categoria.ID == 0)
                    Categoria.Descricao = palavra.Categoria;
            }

            return this;
        }
    }
}