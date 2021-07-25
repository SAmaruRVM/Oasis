using System.Collections.Generic;
using Oasis.Dominio.Entidades;

namespace Oasis.Web.ViewModels
{
    public class GrupoDisciplinaViewModel
    {
        public EscolaViewModel EscolaViewModel { get; set; }
        
        public IEnumerable<Reacao> Reacoes { get; set; }
        
        
    }
}