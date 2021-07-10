using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Dominio.Entidades
{
    public class Reacao
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(20)]
        public string CssClassIcone { get; set; }

        public ICollection<PostGostoUtilizador> PostsQueFizeramUsoDestaReacao { get; } = new List<PostGostoUtilizador>();   
    }
}
