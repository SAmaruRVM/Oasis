using System.Collections.Generic;
using Oasis.Dominio.Entidades;

namespace Oasis.Web.ViewModels
{
    public class PostEspecificoViewModel
    {
        public IEnumerable<Reacao> Reacoes { get; set; }

        public Post Post { get; set; }
    }
}