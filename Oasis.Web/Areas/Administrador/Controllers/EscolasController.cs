using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oasis.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class EscolasController : Controller
    {
        private readonly OasisContext _context;
        public EscolasController(OasisContext context) => (_context) = (context);

        [HttpGet]
        public async Task<ViewResult> Index() => View(await _context.Escolas
                                                  .AsNoTracking()
                                                  .OrderBy(escola => escola.Nome)
                                                  .ToListAsync());
    }
}
