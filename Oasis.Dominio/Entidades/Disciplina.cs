using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Oasis.Dominio.Entidades
{
    public class Disciplina
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome tem que ser obrigatóriamente preenchido!")]
        [StringLength(200, ErrorMessage = "O nome {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Nome", Prompt = "Introduza o nome")]
        [RegularExpression("^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$", ErrorMessage = "O nome da disciplina não poderá conter caracteres especiais!")]
        public string Nome { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataCriacao { get; set; }

        public ApplicationUser CriadorDirecao { get; set; }

        public Escola Escola { get; set; }

        public int ApplicationUserId { get; set; }

        public int EscolaId { get; set; }

        [JsonIgnore]
        public ICollection<Grupo> Grupos { get; } = new List<Grupo>();
    }
}
