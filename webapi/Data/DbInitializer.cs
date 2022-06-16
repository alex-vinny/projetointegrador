using System.Linq;

namespace ProjetoIntegrador.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BancoContext context)
        {
            context.Database.EnsureCreated();

            // Verificar dados existem
            if (context.Usuarios.Any())
                return;
        }
    }
}