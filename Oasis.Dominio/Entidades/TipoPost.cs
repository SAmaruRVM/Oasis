using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Oasis.Dominio.Entidades
{
    public class TipoPost
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome tem que ser obrigatóriamente preenchido!")]
        [StringLength(20, ErrorMessage = "O nome {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Nome", Prompt = "Introduza o nome")]
        public string Nome { get; set; }

        public Grupo Grupo { get; set; }
        public int GrupoId { get; set; }

        [JsonIgnore]
        public ICollection<Post> Posts { get; } = new List<Post>();
    }
}
