using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public ICollection<Post> Posts { get; } = new List<Post>();
    }
}
