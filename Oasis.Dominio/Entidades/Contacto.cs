using System;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Dominio.Entidades
{
    public class Contacto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Assunto", Prompt = "Introduza o assunto do contacto")]
        public string Assunto { get; set; }

        [Required]
        [StringLength(200)]
        public string Descricao { get; set; }

        [Required]
        [EmailAddress]
        public string EmailContactante { get; set; }


        [Required]
        [StringLength(20)]
        public string PrimeiroNome { get; set; }

        [Required]
        [StringLength(20)]
        public string Apelido { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataContacto { get; set; }

        public RespostaContacto RespostaContacto { get; set; }
    }
}
