using Microsoft.AspNetCore.Mvc;
using ProjetoIntegrador.Api.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoIntegrador.Api.Extensions
{
    public static class ControllerExtensions
    {
        public static ResponseDto CreateResult(this ControllerBase controller, string name, object value)
        {
            return new ResponseDto
            {
               { name, value }
            };
        }

        public static ActionResult SendResponse(this ControllerBase controller, ResponseDto response)
        {
            if (response.ContainsKey("erro"))
            {
                var erro = (ErrorDto)response["erro"];
                if(erro != null)
                {
                    switch (erro.Codigo)
                    {
                        case ErrorTypes.NotAllowed:
                            return controller.UnprocessableEntity(response);
                        case ErrorTypes.NotFound:
                        case ErrorTypes.Null:
                            return controller.NotFound(response);
                        case ErrorTypes.NoContent:
                            return controller.NoContent();                            
                        case ErrorTypes.Unknown:
                        case ErrorTypes.BadRequest:
                        default:
                            return controller.BadRequest(response);
                    }
                }

                return controller.BadRequest(response);
            }

            if( response.Any())
            {
                return controller.Ok(response);
            }

            return controller.NoContent();
        }

        public static ActionResult SendResponseLista(this ControllerBase controller, List<ResponseDto> lista)
        {
            
            var response = lista.First();
            if (response.ContainsKey("erro"))
            {
                var erro = (ErrorDto)response["erro"];
                if(erro != null)
                {
                    switch (erro.Codigo)
                    {
                        case ErrorTypes.NotAllowed:
                            return controller.UnprocessableEntity(response);
                        case ErrorTypes.NotFound:
                        case ErrorTypes.Null:
                            return controller.NotFound(response);
                        case ErrorTypes.NoContent:
                            return controller.NoContent();                            
                        case ErrorTypes.Unknown:
                        case ErrorTypes.BadRequest:
                        default:
                            return controller.BadRequest(response);
                    }
                }

                return controller.BadRequest(response);
            }

            if( lista.Any())
            {
                return controller.Ok(lista);
            }

            return controller.NoContent();
        }
    }
  
}
