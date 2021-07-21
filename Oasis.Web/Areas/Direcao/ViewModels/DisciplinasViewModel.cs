using Oasis.Dominio.Entidades;
using System.Collections.Generic;
namespace Oasis.Web.Areas.Direcao.ViewModels
{
    public sealed class DisciplinasViewModel
    {
        public IEnumerable<Disciplina> Disciplinas { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}
