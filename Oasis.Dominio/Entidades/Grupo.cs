using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Dominio.Entidades
{
    public class Grupo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(150)]
        public string Descricao { get; set; }

        public byte[] Banner { get; set; }

        public Disciplina Disciplina { get; set; }
        public ApplicationUser Professor { get; set; }

        public int DisciplinaId { get; set; }
        public int ProfessorId { get; set; }

        public ICollection<GrupoAluno> Alunos { get; } = new List<GrupoAluno>();
        public ICollection<TipoPost> Flairs { get; } = new List<TipoPost>();
    }
}
