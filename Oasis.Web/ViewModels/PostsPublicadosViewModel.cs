using System.Collections.Generic;
using Oasis.Dominio.Entidades;

namespace Oasis.Web.ViewModels
{
    public class PostsPublicadosViewModel
    {
          public IEnumerable<Post> PostsPublicados { get; set; }
    }
}