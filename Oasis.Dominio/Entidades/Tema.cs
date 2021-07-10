using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Oasis.Dominio.Entidades
{
    public class Tema
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        [Url]
        public string LinkCdn { get; set; }

        public ICollection<ApplicationUser> Utilizadores { get; } = new List<ApplicationUser>();
    }
}
