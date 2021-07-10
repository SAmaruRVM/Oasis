using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Dominio.Entidades
{
    public class Escola
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(30)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        public string Rua { get; set; }

        [Required]
        [StringLength(20)]
        public string Distrito { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}(-\d{3})?$")]
        [DataType(DataType.PostalCode)]
        public string CodigoPostal { get; set; }

        [Required]
        [StringLength(9)]
        [DataType(DataType.PhoneNumber)]
        public string ContactoTelefonico { get; set; }
       
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public PaginaPrincipal ConteudoPaginaPrincipal { get; set; }

        public ICollection<DirecaoEscola> Diretores { get; } = new List<DirecaoEscola>();
        public ICollection<Disciplina> Disciplinas { get; } = new List<Disciplina>();
        public ICollection<Equipamento> Equipamentos { get; } = new List<Equipamento>();
    }
}
