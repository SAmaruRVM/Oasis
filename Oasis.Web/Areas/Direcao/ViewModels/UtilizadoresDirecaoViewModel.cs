using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Oasis.Dominio.Entidades;

namespace Oasis.Web.Areas.Direcao.ViewModels
{
    public sealed class UtilizadoresDirecaoViewModel
    {
        public ApplicationUser Utilizador { get; set; }

        public IEnumerable<SelectListItem> TiposUtilizadorDropdownList { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue)]
        [Display(Name = "Qual é o tipo de utilizador que pretende inserir?")]
        public int TiposUtilizadorId { get; set; }

        public IEnumerable<ApplicationUser> UtilizadoresRoles { get; set; }

        [Required(ErrorMessage = "O email tem que ser obrigatóriamente preenchido!")]
        [EmailAddress(ErrorMessage = "O email introduzido encontra-se num formato inválido!")]
        [Display(Name = "Email", Prompt = "Introduza o email")]
        [Remote(action: "EmailEValido", controller: "Conta", areaName: "", ErrorMessage = "O email indicado já se encontra em uso.")]
        public string Email { get; set; }
    }
}