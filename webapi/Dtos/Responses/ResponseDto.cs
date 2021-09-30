using ProjetoIntegrador.Api.Config;
using ProjetoIntegrador.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoIntegrador.Api.Dtos
{
    public class ResponseDto : ModelDto
    {
        public ErrorDto Erros { get; set; }

        public void AddError(string msg, ErrorTypes type = ErrorTypes.Unknown)
        {
            if (Erros == null)
                Erros = new ErrorDto();
            Erros.Code = type;
            Erros.Messages.Add(msg);
        }

        public static ResponseDto Ok()
        {
            return new ResponseDto();            
        }

        public static ResponseDto Exception(ErrorTypes type, params string[] messages)
        {
            var dto = new ResponseDto();
            dto.Erros = new ErrorDto();
            dto.Erros.Code = type;
            
            if(messages.Any())
            {
                foreach (var msg in messages)
                    dto.Erros.Messages.Add(msg);
            }

            return dto;
        }
    }
}