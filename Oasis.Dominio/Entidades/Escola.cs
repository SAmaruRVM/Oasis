using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oasis.Dominio.Entidades
{
    public class Escola
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O código tem que ser obrigatóriamente preenchido!")]
        [StringLength(20, ErrorMessage = "O {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Código", Prompt = "Introduza o código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "O nome tem que ser obrigatóriamente preenchido!")]
        [StringLength(100, ErrorMessage = "O {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Nome", Prompt = "Introduza o nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A rua tem que ser obrigatóriamente preenchida!")]
        [StringLength(100, ErrorMessage = "A {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Rua", Prompt = "Introduza a rua")]
        [DataType(DataType.MultilineText)]
        public string Rua { get; set; }

        [Required(ErrorMessage = "O distrito tem que ser obrigatóriamente preenchido!")]
        [StringLength(20, ErrorMessage = "O {0} tem que ter no máximo {1} caracteres!")]
        [Display(Name = "Distrito", Prompt = "Introduza o distrito")]
        public string Distrito { get; set; }

        [Required(ErrorMessage = "O código postal tem que ser obrigatóriamente preenchido!")]
        [RegularExpression(@"^\d{4}(-\d{3})?$", ErrorMessage = "O código postal introduzido encontra-se num formato inválido!")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Código Postal", Prompt = "Introduza o código postal (formato português)")]
        public string CodigoPostal { get; set; }

        [Required(ErrorMessage = "O contacto telefónico tem que ser obrigatóriamente preenchido!")]
        [StringLength(maximumLength: 9, MinimumLength = 9, ErrorMessage = "O {0} tem que ter obrigatóriamente {1} caracteres!")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contacto telefónico", Prompt = "Introduza o contacto telefónico (+351)")]
        public string ContactoTelefonico { get; set; }


        [Required(ErrorMessage = "O email tem que ser obrigatóriamente preenchido!")]
        [EmailAddress(ErrorMessage = "O email introduzido encontra-se num formato inválido!")]
        [Display(Name = "Email", Prompt = "Introduza o email")]
        public string Email { get; set; }

        // default getdate() <<-- SQL
        public DateTime DataCriacao { get; set; }

        public PaginaPrincipal ConteudoPaginaPrincipal { get; set; }
        public int? PaginaPrincipalId { get; set; }

        public ICollection<ApplicationUser> Membros { get; } = new List<ApplicationUser>();
        public ICollection<Disciplina> Disciplinas { get; } = new List<Disciplina>();
        public ICollection<Equipamento> Equipamentos { get; } = new List<Equipamento>();

        // Not mapped properties

        [NotMapped]
        public string NomeEscolaUrl => string.Join('-', Nome.ToLower().Split(' '));
    }
}
