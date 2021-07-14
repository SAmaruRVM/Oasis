using Oasis.Dominio.Entidades;
using System.Collections.Generic;

namespace Oasis.Web.Areas.Administrador.ViewModels
{
    public sealed class EscolasViewModel
    {
        public IEnumerable<Escola> Escolas { get; set; }
        public Escola EscolaAdicionar { get; set; }
    }
}
