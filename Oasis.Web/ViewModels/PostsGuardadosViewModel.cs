using System.Collections.Generic;
using Oasis.Dominio.Entidades;

namespace Oasis.Web.ViewModels
{
    public class PostsGuardadosViewModel
    {
        public IEnumerable<PostUtilizadorGuardado> PostsGuardados { get; set; }
    }
}