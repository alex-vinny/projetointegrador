using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;

namespace ProjetoIntegrador.Api.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public static IQueryable Query(this DbContext context, string entityName) =>
                context.Query(context.Model.FindEntityType(entityName).ClrType);
        
        public static IQueryable Query(this DbContext context, Type entityType) =>
                (IQueryable)((IDbSetCache)context).GetOrAddSet(context.GetDependencies().SetSource, entityType);        
    }
}
