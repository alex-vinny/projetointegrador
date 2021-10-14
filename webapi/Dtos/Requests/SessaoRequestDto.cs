using System;

namespace ProjetoIntegrador.Api.Dtos
{
    public class SessaoRequestDto : IModelDto
    {
        public int Id { get; set; }
        public int? AdicionarAcertos { get; set; }
        public int? TotalAcertos { get; set; }
        public DateTime? DataFim { get; internal set; }
    }
}