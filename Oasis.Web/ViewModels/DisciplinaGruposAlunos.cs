using System.Collections.Generic;
using Oasis.Dominio.Entidades;

namespace Oasis.Web.ViewModels
{
    public class DisciplinaGruposAlunos
    {
       public string NomeDisciplina { get; set; }
 
       public IEnumerable<GrupoAluno> Grupos { get; set; }
    }
}