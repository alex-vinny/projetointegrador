using ProjetoIntegrador.Api.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoIntegrador.Api.Models
{
    public enum Perfis
    {
        [Display(Name = "ALUNO")]
        Aluno,
        [Display(Name = "PROFESSOR")]
        Professor,
        [Display(Name = "ADMINISTRADOR")]
        Administrador,
        [Display(Name = "CONVIDADO")]
        Convidado
    }

    public enum Sexos
    {
        [Display(Name = "M")]
        Masculino,
        [Display(Name = "F")]
        Feminino,
        [Display(Name = "O")]
        Outro
    }

    public class Usuario
    {
        public Usuario() { }

        public int ID { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public Perfis Perfil { get; set; }
        public string DicaSecreta { get; set; }
        public string PalavraSecreta { get; set; }
        
        public ResponseDto MakeResponse()
        {
            return new ResponseDto
            {
                { "id", ID },
                { "email", Email },
                { "nome", Nome },
                {
                    "perfil",
                    new 
                    {
                        codigo = (int)Perfil,
                        descricao = Perfil.ToString()
                    }
                }
            };
        }
    }
}