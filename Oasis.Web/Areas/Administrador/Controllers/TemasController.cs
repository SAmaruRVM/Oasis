using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oasis.Dados;
using System;
using System.Collections.Generic;
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
    }
}
