using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oasis.Dados;
using Oasis.Web.Areas.Administrador.ViewModels;
using Oasis.Web.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Administrador.Controllers
{
    public class TemasController : BaseAdministradorController
    {
        private readonly OasisContext _context;
        public TemasController(OasisContext context) => (_context) = (context);

        [HttpGet]
        public async Task<ViewResult> Index() => View(model: new TemasViewModel 
        {
            Temas = await _context.Temas
                                  .AsNoTracking()
                                  .OrderBy(tema => tema.Nome)
                                  .ToListAsync()
        });

        [HttpPost]
        public async Task<JsonResult> EditarTemas([FromForm] int n)
        {
            return Json(string.Empty);
        }


        [HttpPost]
        public async Task<JsonResult> Criar([FromForm] TemasViewModel temasViewModel)
        {
            if (!(ModelState.IsValid))
            {
                return Json(new Ajax
                {
                    Titulo = "Erro ao adicionar o tema!",
                    Descricao = "Os dados indicados não se encontram num formato válido!",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            _context.Temas.Add(temasViewModel.TemaAdicionar);
            await _context.SaveChangesAsync();


            return Json(new
            {
                Ajax = new Ajax
                {
                    Titulo = "Sucesso ao adicionar o tema!",
                    Descricao = "O tema foi adicionado com sucesso.",
                    OcorreuAlgumErro = false,
                    UrlRedirecionar = string.Empty
                },
                Tema = temasViewModel.TemaAdicionar
            });
        }
    }
}
