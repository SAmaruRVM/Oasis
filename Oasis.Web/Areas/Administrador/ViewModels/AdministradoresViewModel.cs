using Microsoft.AspNetCore.Mvc;
using Oasis.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Administrador.ViewModels {
    public class AdministradoresViewModel {
        public ApplicationUser Administrador { get; set; }

        [Required(ErrorMessage = "O email tem que ser obrigatóriamente preenchido!")]
        [EmailAddress(ErrorMessage = "O email introduzido encontra-se num formato inválido!")]
        [Display(Name = "Email", Prompt = "Introduza o email")]
        [Remote(action: "EmailEValido", controller: "Conta", areaName: "", ErrorMessage = "O email indicado já se encontra em uso.")]
        public string Email { get; set; }

        public IEnumerable<ApplicationUser> Utilizadores { get; set; }
        public int AdministradorEliminarId { get; set; }
    }
}
