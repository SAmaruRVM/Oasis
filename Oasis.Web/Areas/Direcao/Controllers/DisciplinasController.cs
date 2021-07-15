using Microsoft.AspNetCore.Mvc;
using Oasis.Dominio.Entidades;
using Oasis.Web.Http;

namespace Oasis.Web.Areas.Direcao.Controllers
{
    [Area("Direcao")]
    public class DisciplinasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Criar() => View();

        [HttpPost]
        public JsonResult Inserir([FromForm] Disciplina disciplina) 
        {
            return Json(new Ajax
            {
                Titulo = "Os dados indicados não se encontram num formato válido!",
                Descricao = "Por favor, introduza os dados corretamente.",
                OcorreuAlgumErro = true,
                UrlRedirecionar = string.Empty
            });
        }
    }
}
