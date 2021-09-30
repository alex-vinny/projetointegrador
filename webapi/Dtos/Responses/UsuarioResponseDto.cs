using ProjetoIntegrador.Api.Models;
using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Dtos
{
    public class UsuarioResponseDto : ResponseDto
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public Perfis Perfil { get; set; }
        public string DicaSecreta { get; set; }
        public string PalavraSecreta { get; set; }

        public Usuario ToModel(string email)
        {
            return new Usuario
            {
                ID = Id,
                DicaSecreta = DicaSecreta,
                Email = email,
                Nome = Nome,
                Perfil = Perfil,
                PalavraSecreta = PalavraSecreta,
                Senha = Senha
            };
        }

        public UsuarioDto UpdateFrom(UsuarioRequestDto dto)
        {
            if(dto != null)
            {
                Nome = dto.Nome;
                Senha = dto.Senha;
                Perfil = dto.Perfil;
                DicaSecreta = dto.DicaSecreta;
                PalavraSecreta = dto.PalavraSecreta;
            }

            return this;
        }
    }
}