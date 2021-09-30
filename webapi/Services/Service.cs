using ProjetoIntegrador.Api.Dtos;
using System;

namespace ProjetoIntegrador.Api.Services
{
    public abstract class Service
    {
        protected static T Null<T>(string msg)
            where T : ResponseDto, new()
        {
            var resp = new T();
            resp.AddError(msg, ErrorTypes.Null);

            return resp;
        }

        protected T Exception<T>(Exception ex)
            where T : ResponseDto, new()
        {
            var resp = new T();
            resp.AddError(ex.Message, ErrorTypes.BadRequest);

            return resp;
        }
    }
}
