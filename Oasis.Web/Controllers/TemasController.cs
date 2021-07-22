using Microsoft.AspNetCore.Mvc;
using Oasis.Dados;
using Oasis.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.Controllers {
    public class TemasController : Controller {

        private readonly OasisContext _context;
        public TemasController(OasisContext context) => (_context) = (context);
        public IActionResult Index() {
            IEnumerable<Tema> temas = _context.Temas;
            return View(temas);
        }
    }
}
