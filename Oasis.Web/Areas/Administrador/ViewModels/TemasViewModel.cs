using System.Collections.Generic;
using Oasis.Dominio.Entidades;

namespace Oasis.Web.Areas.Administrador.ViewModels
{
    public sealed class TemasViewModel
    {
        public IEnumerable<Tema> Temas { get; set; }
        public Tema TemaAdicionar { get; set; }
    }
}