using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oasis.Dominio.Entidades
{
    public class Report
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O assunto tem que ser obrigatóriamente preenchido!")]
        [StringLength(20, ErrorMessage = "O {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Assunto", Prompt = "Introduza o assunto")]
        public string Assunto { get; set; }

        [Required(ErrorMessage = "A descrição tem que ser obrigatóriamente preenchido!")]
        [StringLength(200, ErrorMessage = "A descrição tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Descrição", Prompt = "Introduza a descrição")]
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
