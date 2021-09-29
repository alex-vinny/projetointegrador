using ProjetoIntegrador.Api.Models;
using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Dto
{
    public class Dto
    {
        public int Id { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public static Dto DefaultPagination => new Dto { Skip = 0, Take = 5000 };
    }
}