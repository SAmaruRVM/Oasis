using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Dominio.Entidades
{
    public class Disciplina
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataCriacao { get; set; }

        public ApplicationUser CriadorDirecao { get; set; }

        public Escola Escola { get; set; }

        public int ApplicationUserId { get; set; }

        public int EscolaId { get; set; }

        public ICollection<Grupo> Grupos { get; } = new List<Grupo>();
    }
}
