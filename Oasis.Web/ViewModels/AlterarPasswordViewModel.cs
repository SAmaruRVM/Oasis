using System.ComponentModel.DataAnnotations;

namespace Oasis.Web.ViewModels
{
    public sealed class AlterarPasswordViewModel
    {
        [Required(ErrorMessage = "A password atual tem que ser obrigatoriamente preenchida!")]
        [Display(Name = "A sua password atual", Prompt = "Introduza a sua password atual")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "A nova password tem que ser obrigatoriamente preenchida!")]
        [Display(Name = "A sua nova password", Prompt = "Introduza a sua nova password")]
        [DataType(DataType.Password)]
        public string NovaPassword { get; set; }

        [Required(ErrorMessage = "A confirmação da nova password tem que ser obrigatoriamente preenchida!")]
        [Display(Name = "Confirme a sua nova password", Prompt = "Introduza a confirmação da nova password")]
        [Compare(nameof(NovaPassword), ErrorMessage = "A confirmação da sua nova password tem que ser igual à nova password introduzida!")]
        [DataType(DataType.Password)]
        public string ConfirmacaoNovaPassword { get; set; }
    }
}