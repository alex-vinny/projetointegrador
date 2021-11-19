using ProjetoIntegrador.Api.Models;
using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Dtos
{
    public class PalavraDto : PalavraRequestDto
    { 
        public int QtdItens { get; set; }
        public bool InformarCategoria { get; set; }
    }
}