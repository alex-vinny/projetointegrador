using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Dtos
{
    public enum ErrorTypes
    {
        Unknown,
        Null,
        NotFound,
        NoContent,
        BadRequest,
        NotAllowed
    }

    public class ErrorDto
    {
        public ErrorDto()
        {
            Codigo = ErrorTypes.Unknown;
            Mensagens = new List<string>();
        }
        public ErrorTypes Codigo { get; set; }
        public List<string> Mensagens { get; set; }
    }
}
