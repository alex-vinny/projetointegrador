using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Models
{
    public enum Orientacao
    {
        Horizontal,
        Vertical
    }

    public class CruzadaItem
    {
        public int ID { get; set; }
        public Palavra Palavra {get; set; }
        public int PosicaoX { get; set; }
        public int PosicaoY { get; set; }
        public Orientacao Orientacao { get; set; }
    }
}