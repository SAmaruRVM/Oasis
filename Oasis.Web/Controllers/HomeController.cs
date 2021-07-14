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

namespace Oasis.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly OasisContext _context;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public HomeController(IConfiguration configuration, OasisContext context, SignInManager<ApplicationUser> signInManager)
            => (_configuration, _context, _signInManager) = (configuration, context, signInManager);

        [HttpGet]
        public ViewResult Index() => View();


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

            SmtpClient smtpClient = new();

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
    }
}
