using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Models
{
    public enum Perfis
    {
        Aluno,
        Professor,
        Administrador,
        Convidado
    }

    public enum Sexos
    {
        Masculino,
        Feminino
    }

    public class Usuario
    {
        public Usuario() : this(Perfis.Convidado) { }
        
        public Usuario(Perfis perfil)
        {
            Perfil = perfil;
        }

        public int ID { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public Perfis Perfil { get; }        
    }
}