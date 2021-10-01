using ProjetoIntegrador.Api.Config;
using ProjetoIntegrador.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoIntegrador.Api.Dtos
{
    public class ResponseDto : Dictionary<string, object>
    {
        public void AddError(ErrorTypes type = ErrorTypes.Unknown, params string[] messages)
        {
            if (!ContainsKey("Error"))
                Add("Error", null);
            
            ErrorDto erros = (this["Erros"] == null ? new ErrorDto() : (ErrorDto)this["Erros"]);
            erros.Codigo = type;

            if (messages.Any())
            {
                foreach (var msg in messages)
                    erros.Mensagens.Add(msg);
            }
        }

        public static ResponseDto Ok()
        {
            return new ResponseDto
            {
                {"Sucesso", true }
            };
        }
    }
}