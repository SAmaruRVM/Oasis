using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Dominio.Entidades
{
    public class TipoPost
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Nome { get; set; }

        public Grupo Grupo { get; set; }
        public int GrupoId { get; set; }

        public ICollection<Post> Posts { get; } = new List<Post>();
    }
}
