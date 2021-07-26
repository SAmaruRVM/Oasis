using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Oasis.Dominio.Entidades
{
    public class Tema
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome tem que ser obrigatóriamente preenchido!")]
        [StringLength(20, ErrorMessage = "O {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Nome", Prompt = "Introduza o nome")] 
        public string Nome { get; set; }

        [Required(ErrorMessage = "O link tem que ser obrigatóriamente preenchido!")]
        [StringLength(100, ErrorMessage = "O {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Link CDN", Prompt = "Link CDN (Content Delivery Network)")] 
        [Url(ErrorMessage = "O link introduzido encontra-se num formato inválido!")]
        [DataType(DataType.Url)]
        public string LinkCdn { get; set; }

        [JsonIgnore]
        public ICollection<ApplicationUser> Utilizadores { get; } = new List<ApplicationUser>();
    }
}