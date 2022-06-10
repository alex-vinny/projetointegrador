using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ProjetoIntegrador.Api.Models.Comparers
{
    public class PalavraComparador : IEqualityComparer<Palavra>
    {
        public bool Equals([AllowNull] Palavra x, [AllowNull] Palavra y)
        {
            return x.ValorSemAcento.Equals(y.ValorSemAcento);
        }

        public int GetHashCode([DisallowNull] Palavra obj)
        {
            return obj.ID.GetHashCode();
        }
    }
}
