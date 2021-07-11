using Microsoft.AspNetCore.Mvc;
using Oasis.Dados;
using Oasis.Dominio.Entidades;
using Oasis.Web.Http;
using Oasis.Web.ViewModels;
using System.Threading.Tasks;

namespace Oasis.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly OasisContext _context;
        public HomeController(OasisContext context) => (_context) = (context);

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
                    Titulo = "Os dados não são válidos",
                    Descricao = "Erro",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            _context.Contactos.Add(contacto);
            await _context.SaveChangesAsync();
            return Json(new Ajax 
            {
                Titulo = "Contacto enviado",
                Descricao = "Teste",
                OcorreuAlgumErro = false,
                UrlRedirecionar = string.Empty
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Login([FromForm] LoginViewModel loginViewModel) => Json(string.Empty);
    }
}
