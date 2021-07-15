using Identity = Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Oasis.Web.ViewModels;
using System.Threading.Tasks;
using Oasis.Dados;
using Microsoft.Extensions.Configuration;
using Oasis.Dominio.Entidades;
using Microsoft.AspNetCore.Identity;
using Oasis.Web.Http;
using Microsoft.EntityFrameworkCore;

namespace Oasis.Web.Controllers
{
    public class ContaController : Controller
    {
        private readonly OasisContext _context;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ContaController(IConfiguration configuration, OasisContext context, SignInManager<ApplicationUser> signInManager)
            => (_configuration, _context, _signInManager) = (configuration, context, signInManager);


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Login([FromForm] LoginViewModel loginViewModel)
        {
            if (!(ModelState.IsValid))
            {
                return Json(new Ajax
                {
                    Titulo = "Os dados indicados não se encontram num formato válido!",
                    Descricao = "Por favor, introduza os dados corretamente.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            var utilizadorTentativaLogin = await _context.Utilizadores
                                                         .Include(utilizador => utilizador.Escola)
                                                         .SingleOrDefaultAsync(utilizador => utilizador.Email == loginViewModel.Email);
    

            if (utilizadorTentativaLogin is null)
            {
                return Json(new Ajax
                {
                    Titulo = "Os dados fornecidos para autenticação não estão corretos!",
                    Descricao = "Por favor, introduza o email e a password corretamente.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user: utilizadorTentativaLogin, password: loginViewModel.Password, isPersistent: loginViewModel.LembrarDeMim, lockoutOnFailure: false);

            if (!(result.Succeeded))
            {
                return Json(new Ajax
                {
                    Titulo = "Os dados fornecidos para autenticação não estão corretos!",
                    Descricao = "Por favor, introduza o email e a password corretamente.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }


            if (!(utilizadorTentativaLogin.EmailConfirmed))
            {
                return Json(new Ajax
                {
                    Titulo = "O seu email ainda não foi confirmado.",
                    Descricao = "Para que possa entrar na sua conta, terá que ter o seu email confirmado. Por favor, verifique o seu email ou entre em contacto connosco.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            return Json(new Ajax
            {
                Titulo = "Login foi realizado som ceusso!",
                Descricao = "Será agora redirecionado para a página da sua escola.",
                OcorreuAlgumErro = false,
                UrlRedirecionar = $"escola/{utilizadorTentativaLogin.Escola.NomeEscolaUrl}"
            });
        }
    }
}
