using ProjetoIntegrador.Api.Dtos;
using System;
using System.Linq;

namespace ProjetoIntegrador.Api.Services
{
    public abstract class Service
    {
        protected ResponseDto Null(string msg)
        {
            return MakeErrorResponse(ErrorTypes.Null, msg);
        }

        protected ResponseDto Exception(Exception ex)
        {
            return MakeErrorResponse(ErrorTypes.BadRequest, ex.Message);
        }

        protected ResponseDto Error(string msg)
        {
            return MakeErrorResponse(ErrorTypes.BadRequest, msg);
        }

        protected ResponseDto ErrorResponse(ErrorTypes type, params string[] messages)

        {
            return MakeErrorResponse(type, messages);
        }
        
        private ResponseDto MakeErrorResponse(ErrorTypes type, params string[] messages)
        {
            var erro = new ErrorDto();
            erro.Codigo = type;
            
            if(messages.Any())
            {
                foreach (var message in messages)
                {
                    erro.Mensagens.Add(message);
                }
            }

            return new ResponseDto
            {
                { "erro", erro }
            };
        }
    }
}
