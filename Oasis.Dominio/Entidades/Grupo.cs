using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Oasis.Dominio.Entidades
{
    public class Grupo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome tem que ser obrigatóriamente preenchido!")]
        [StringLength(50, ErrorMessage = "O {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Nome", Prompt = "Introduza o nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição tem que ser obrigatóriamente preenchida!")]
        [StringLength(150, ErrorMessage = "A {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Descrição", Prompt = "Introduza a descrição")]
        public string Descricao { get; set; }

        public byte[] Banner { get; set; }

        public Disciplina Disciplina { get; set; }
        public ApplicationUser Professor { get; set; }

        public int DisciplinaId { get; set; }
        public int ProfessorId { get; set; }

        [JsonIgnore]
        public ICollection<GrupoAluno> Alunos { get; } = new List<GrupoAluno>();

        [JsonIgnore]
        public ICollection<TipoPost> Flairs { get; } = new List<TipoPost>();

        [JsonIgnore]

        public ICollection<Post> Posts { get; } = new List<Post>();
    }
}
