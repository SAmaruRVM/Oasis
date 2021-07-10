using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oasis.Dominio.Entidades
{
    public class Report
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(200)]
        public string Descricao { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataCriacao { get; set; }

        [ForeignKey(nameof(AlunoId))]
        public ApplicationUser Aluno { get; set; }

        [ForeignKey(nameof(ProfessorId))]
        public ApplicationUser Professor { get; set; }
 
        public int? AlunoId { get; set; }

        public int? ProfessorId { get; set; }

    }
}
