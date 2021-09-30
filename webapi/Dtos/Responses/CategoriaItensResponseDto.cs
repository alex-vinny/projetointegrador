using ProjetoIntegrador.Api.Models;
using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Dtos
{
    public class CategoriaItensResponseDto : ResponseDto
    {
        public List<CategoriaDto> Categorias { get; set; }
    }
}