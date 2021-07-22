using System.Collections.Generic;

namespace Oasis.Web.ViewModels
{
    public class EscolaViewModel
    {
        public AlterarPasswordViewModel AlterarPasswordViewModel { get; set; }

        public IEnumerable<DisciplinaGruposAlunos> DisciplinaGruposAlunos  { get; set; }

    }
}