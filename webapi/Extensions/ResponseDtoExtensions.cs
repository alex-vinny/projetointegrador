using ProjetoIntegrador.Api.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoIntegrador.Api.Extensions
{
    public static class ResponseDtoExtensions
    {
        public static List<ResponseDto> AsList(this ResponseDto dto)
        {
            return new[] { dto }.ToList();
        }
    }
}
