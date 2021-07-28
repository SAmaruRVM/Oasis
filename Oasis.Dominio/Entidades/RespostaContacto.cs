using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Oasis.Dominio.Entidades
{
    public class RespostaContacto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A resposta ao contacto tem que ser obrigatóriamente preenchida!")]
        [StringLength(200, ErrorMessage = "A {0} ao contacto tem que ter no máximo {1} caracteres!")]
        [Display(Prompt = "Introduza a resposta ao contacto")]
        public string Resposta { get; set; }

        [JsonIgnore]
        public Contacto Contacto { get; set; }

        public int ContactoId { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataResposta { get; set; }
    }
}