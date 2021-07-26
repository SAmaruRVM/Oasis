using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Oasis.Dominio.Entidades
{
    public class Reacao
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string Titulo { get; set; }

        [StringLength(100)]
        public string Icone { get; set; }

        [JsonIgnore]
        public ICollection<PostGostoUtilizador> PostsQueFizeramUsoDestaReacao { get; } = new List<PostGostoUtilizador>();   
    }
}
