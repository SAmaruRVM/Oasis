﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oasis.Dados;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Administrador.Controllers
{
    public class ContactosController : BaseAdministradorController
    {
        private readonly OasisContext _context;
        public ContactosController(OasisContext context) => (_context) = (context);

        [HttpGet]
        public async Task<ViewResult> Index() => View(await _context.Contactos
                                                                    .AsNoTracking()
                                                                    .OrderByDescending(contacto => contacto.DataContacto)
                                                                    .ToListAsync());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> RespostaContacto([FromForm] int n)
        {
            return Json(string.Empty);
        }
    }
}
