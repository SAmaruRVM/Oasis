using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using Oasis.Dominio.Entidades;

namespace Oasis.Web.ViewModels
{
    public class GruposViewModel
    {
        public IEnumerable<Grupo> Grupos { get; set; }

        public Grupo GrupoAlterar { get; set; }
    }
}