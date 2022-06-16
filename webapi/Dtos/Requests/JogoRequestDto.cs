using ProjetoIntegrador.Api.Extensions;
using ProjetoIntegrador.Api.Models;
using System;

namespace ProjetoIntegrador.Api.Dtos
{
    public class JogoRequestDto
    {
        object filtro;
        public JogoRequestDto(object filtro)
        {
            this.filtro = filtro;
        }

        public override string ToString()
        {
            return filtro.ToString(); 
        }

        public bool IsInteger()
        {
            return ToString().IsNumeric();
        }

        public int GetValor()
        {
            int.TryParse(ToString(), out var valor);
            return valor;
        }

        public bool IsText()
        {
            return typeof(string) == filtro.GetType();
        }

        public bool IsEnumConvertible()
        {
            return Enum.TryParse(typeof(TiposJogo), ToString(), out _);
        }

        public TiposJogo? GetEnum()
        {
            Enum.TryParse(typeof(TiposJogo), ToString(), out object ret);
            return (TiposJogo)ret;
        }
    }
}