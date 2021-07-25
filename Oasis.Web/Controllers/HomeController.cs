using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Oasis.Aplicacao.Extensions;
using Oasis.Dados;
using Oasis.Dominio.Entidades;
using Oasis.Web.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Oasis.Web.Extensions;
using Oasis.Dominio.Enums;
using Oasis.Web.Areas.Administrador.ViewModels;

namespace Oasis.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly OasisContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public HomeController(IConfiguration configuration, OasisContext context, SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager)
            => (_configuration, _context, _userManager) = (configuration, context, userManager);

        [HttpGet]
        public IActionResult Index()
        {


            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(TipoUtilizador.Administrador.ToString()))
                {
                    return RedirectToAction(
                                       actionName: "Index",
                                       controllerName: "Home",
                                       routeValues: new { area = "Administrador" }
                    );
                }

                return RedirectToAction(
                    actionName: nameof(Index),
                    controllerName: "Escola"
                );
            }

            

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ContacteNos([FromForm] Contacto contacto)
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

            _context.Contactos.Add(contacto);
            await _context.SaveChangesAsync();

            using SmtpClient smtpClient = new();

            await smtpClient.EnviarEmailAsync("Teste email", "Contacto enviado", contacto.EmailContactante, smtpClient.ConfiguracoesEmail(_configuration));
            await smtpClient.EnviarEmailAsync("Teste email", "Uma nova pessoa tentou contactar", "joaopedromane23@gmail.com", smtpClient.ConfiguracoesEmail(_configuration));

            return Json(new Ajax
            {
                Titulo = "O seu contacto foi enviado com sucesso!",
                Descricao = "Por favor, fique atento ao seu email, no qual irá ser contactado por um dos nossos colaboradores.",
                OcorreuAlgumErro = false,
                UrlRedirecionar = string.Empty
            });
        }

        [HttpGet("[controller]/Acesso-Negado")]
        public IActionResult AcessoNegado()
        {
            if (!(string.IsNullOrWhiteSpace(Request.QueryString.Value)))
            {
                return RedirectToAction(nameof(AcessoNegado));
            }

            return View();
        }
    }
}
