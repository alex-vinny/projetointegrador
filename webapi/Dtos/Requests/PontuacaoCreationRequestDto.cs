using System;

namespace ProjetoIntegrador.Api.Dtos
{
    public class PontuacaoCreationRequestDto : PontuacaoRequestDto
    {
        public int? TipoJogoId { get; set; }
        public string TipoJogo { get; set; }
        public string Email { get; set; }
        public int? Usuario { get; set; }       
        public DateTime? DataJogo { get; set; }
    }
}