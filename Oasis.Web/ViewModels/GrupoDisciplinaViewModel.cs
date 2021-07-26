using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Oasis.Dominio.Entidades;

namespace Oasis.Web.ViewModels
{
    public class GrupoDisciplinaViewModel
    {
        public EscolaViewModel EscolaViewModel { get; set; }
        
        public IEnumerable<Reacao> Reacoes { get; set; }

        public Grupo Grupo { get; set; }
        public ComentarioPostUtilizador ComentarioPostUtilizador { get; set; }
        public PostInserirViewModel PostInserirViewModel { get; set; }

        public IEnumerable<SelectListItem> TiposPostDropdownList { get; set; }
        public IEnumerable<Post> PostsGrupo { get; set; }
    }
}