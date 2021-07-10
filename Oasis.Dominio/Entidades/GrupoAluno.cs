
using System;

namespace Oasis.Dominio.Entidades
{
    public class GrupoAluno
    {
        public ApplicationUser Aluno { get; set; }
        public Grupo Grupo { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataInsercao { get; set; }

        public int ApplicationUserId { get; set; }
        public int GrupoId { get; set; }
    }
}
