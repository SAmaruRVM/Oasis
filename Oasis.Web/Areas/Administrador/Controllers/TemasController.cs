using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oasis.Dados;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Administrador.Controllers {
    [Area("Administrador")]
    public class TemasController : Controller {
        private readonly OasisContext _context;
        public TemasController(OasisContext context) => (_context) = (context);

        [HttpGet]
        public async Task<ViewResult> Index() => View(await _context.Temas
                                                  .AsNoTracking()
                                                  .OrderBy(tema => tema.Nome)
                                                  .ToListAsync());

        [HttpPost]
        public async Task<JsonResult> EditarTemas([FromForm] int n) {
            return Json(string.Empty);
        }


        [HttpPost]
        public async Task<JsonResult> Criar([FromForm] TemasController temasController) {
            return Json(string.Empty);
        }
    }
}
