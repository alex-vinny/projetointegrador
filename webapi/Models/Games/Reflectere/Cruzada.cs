using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Models
{
    public class Cruzada
    {
        public int ID { get; set; }
        public int TamanhoX { get; set; }
        public int TamanhoY { get; set; }
        public DateTime Criacao { get; set; }
        public Usuario Autor { get; set; }
        public Categoria Categoria { get; set; }
        public ICollection<CruzadaItem> Itens { get; set; }
    }
}