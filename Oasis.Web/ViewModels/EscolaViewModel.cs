using System.Collections.Generic;
using Oasis.Dominio.Entidades;
namespace Oasis.Web.ViewModels
{
    public class EscolaViewModel
    {
        public AlterarPasswordViewModel AlterarPasswordViewModel { get; set; }

        public IEnumerable<DisciplinaGruposAlunos> DisciplinaGruposAlunos  { get; set; }
        public IEnumerable<GrupoAluno> GruposAulasAlunos  { get; set; }

        public PaginaPrincipal PaginaPrincipal { get; set; }
    
    }
}