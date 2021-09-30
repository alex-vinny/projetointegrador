using ProjetoIntegrador.Api.Models;
using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Dtos
{
    public class UsuarioRequestDto : RequestDto
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public Perfis Perfil { get; set; }
        public string DicaSecreta { get; set; }
        public string PalavraSecreta { get; set; }
    }
}