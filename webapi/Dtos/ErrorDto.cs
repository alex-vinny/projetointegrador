using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Dtos
{
    public enum ErrorTypes
    {
        Unknown,
        Null,
        NotFound,
        NoContent,
        BadRequest
    }

    public class ErrorDto
    {
        public ErrorDto()
        {
            Code = ErrorTypes.Unknown;
            Messages = new List<string>();
        }
        public ErrorTypes Code { get; set; }
        public List<string> Messages { get; set; }
    }
}
