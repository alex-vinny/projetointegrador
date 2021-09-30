using ProjetoIntegrador.Api.Config;
using ProjetoIntegrador.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoIntegrador.Api.Dtos
{
    public class RequestDto
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }        
        public static RequestDto DefaultPagination => new RequestDto { Skip = 0, Take = Constants.TAMANHO_PAGINA };
    }
}