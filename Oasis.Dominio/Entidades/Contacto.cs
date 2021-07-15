using System;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Dominio.Entidades
{
    public class Contacto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O assunto tem que ser obrigatóriamente preenchido!")]
        [StringLength(50, ErrorMessage = "O {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Assunto", Prompt = "Introduza o assunto do contacto")]
        public string Assunto { get; set; }

        [Required(ErrorMessage = "A descrição tem que ser obrigatóriamente preenchido!")]
        //[StringLength(200, ErrorMessage = "A {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Descrição", Prompt = "Introduza a descrição do contacto")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O email tem que ser obrigatóriamente preenchido!")]
        [EmailAddress(ErrorMessage = "O email indicado não se encontra num formato válido!")]
        [Display(Name = "Email", Prompt = "Introduza o seu email")]
        public string EmailContactante { get; set; }


        [Required(ErrorMessage = "O primeiro nome tem que ser obrigatóriamente preenchido!")]
        [StringLength(20, ErrorMessage = "O {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Primeiro Nome", Prompt = "Introduza o seu primeiro nome")]
        public string PrimeiroNome { get; set; }

        [Required(ErrorMessage = "O apelido tem que ser obrigatóriamente preenchido!")]
        [StringLength(20, ErrorMessage = "O {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Apelido", Prompt = "Introduza o seu apelido")]
        public string Apelido { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataContacto { get; set; }

        public RespostaContacto RespostaContacto { get; set; }
    }
}
