using System;

namespace Oasis.Dominio.Entidades
{
    public class DirecaoEscola
    {
        public ApplicationUser Diretor { get; set; }
        public Escola Escola { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataInsercao { get; set; }

        public int ApplicationUserId { get; set; } 
        public int EscolaId { get; set; }
    }
}
