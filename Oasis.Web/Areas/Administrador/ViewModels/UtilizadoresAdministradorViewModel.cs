using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Oasis.Dominio.Entidades;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oasis.Web.Areas.Administrador.ViewModels
{
    public sealed class UtilizadoresAdministradorViewModel
    {
        public ApplicationUser MembroDirecao { get; set; }

        [Required(ErrorMessage = "O email tem que ser obrigatóriamente preenchido!")]
        [EmailAddress(ErrorMessage = "O email introduzido encontra-se num formato inválido!")]
        [Display(Name = "Email", Prompt = "Introduza o email")]
        [Remote(action: "EmailEValido", controller: "Conta", areaName: "", ErrorMessage = "O email indicado já se encontra em uso.")]
        public string Email { get; set; }
        
        public int IdEscola { get; set; }

        public IEnumerable<ApplicationUser> Utilizadores { get; set; }
        public IEnumerable<SelectListItem> EscolasDropdownList { get; set; }
    }
}
