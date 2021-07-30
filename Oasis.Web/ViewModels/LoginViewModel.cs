using System.ComponentModel.DataAnnotations;
namespace Oasis.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O email tem que ser obrigatóriamente preenchido!")]
        [EmailAddress(ErrorMessage = "O email introduzido encontra-se num formato inválido!")]
        [Display(Prompt = "Introduza o seu email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "A password tem que ser obrigatóriamente preenchida!")]
        [DataType(DataType.Password)]
        [Display(Prompt = "Introduza a sua password")]
        public string Password { get; set; }


        [Display(Name = "Lembrar de mim")]
        public bool LembrarDeMim { get; set; }


        [Required(ErrorMessage = "O email para a recuperação de password tem que ser obrigatóriamente preenchido!")]
        [EmailAddress(ErrorMessage = "O email introduzido para a recuperação de password encontra-se num formato inválido!")]
        [Display(Name = "Email", Prompt = "Introduza o seu email para a recuperação de password ")]
        public string EmailRecuperacaoPassword { get; set; }
    }
}
