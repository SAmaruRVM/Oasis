using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Oasis.Aplicacao;
using Oasis.Aplicacao.Extensions;
using Oasis.Dados;
using Oasis.Dominio.Entidades;
using Oasis.Web.Http;
using Oasis.Web.ViewModels;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Oasis.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly OasisContext _context;
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration, OasisContext context) => (_configuration, _context) = (configuration, context);

        [HttpGet]
        public ViewResult Index() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ContacteNos([FromForm] Contacto contacto) 
        {
            if(!(ModelState.IsValid))
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

            await new SmtpClient().EnviarEmailAsync("Teste email", "Contacto enviado", contacto.EmailContactante, _configuracoesEmail());
            await new SmtpClient().EnviarEmailAsync("Teste email", "Uma nova pessoa tentou contactar", "joaopedromane23@gmail.com", _configuracoesEmail());

            return Json(new Ajax 
            {
                Titulo = "O seu contacto foi enviado com sucesso!",
                Descricao = "Por favor, fique atento ao seu email, no qual irá ser contactado por um dos nossos colaboradores.",
                OcorreuAlgumErro = false,
                UrlRedirecionar = string.Empty
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Login([FromForm] LoginViewModel loginViewModel) => Json(string.Empty);


        // Métodos Privados
        private Action<ConfiguracoesEmail> _configuracoesEmail() => configuracoesEmail =>
        {
            configuracoesEmail.Credenciais = new NetworkCredential
            {
                UserName = _configuration["Projeto:Email:User"],
                Password = _configuration["Projeto:Email:Password"]
            };
            configuracoesEmail.Email = _configuration["Projeto:Email:User"];
            configuracoesEmail.SmtpHost = _configuration["Projeto:Email:Smtp-Host"];
            configuracoesEmail.SmtpPort = int.Parse(_configuration["Projeto:Email:Smtp-Port"]);
        };

    }
}
