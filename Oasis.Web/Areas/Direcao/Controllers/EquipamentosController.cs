using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oasis.Dados;
using Oasis.Dominio.Entidades;
using Oasis.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Direcao.Controllers {
    [Area("Direcao")]
    public class EquipamentosController : Controller {

        private readonly OasisContext _context;
        public EquipamentosController(OasisContext context) => (_context) = (context);

        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Inserir() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Inserir([FromForm] Equipamento equipamento) {
            if (!(ModelState.IsValid)) {
                return Json(new Ajax {
                    Titulo = "Os dados indicados não se encontram num formato válido!",
                    Descricao = "Por favor, introduza os dados corretamente.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            equipamento.DirectorResponsavelInsercao = await _context.Utilizadores
                                                                    .Include(utilizador => utilizador.Escola)
                                                                    .SingleOrDefaultAsync(utilizador => utilizador.Email == User.Identity.Name);

            equipamento.Escola = equipamento.DirectorResponsavelInsercao.Escola;


            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();

            return Json(new Ajax {
                Titulo = "Sucesso ao adicionar o equipamento!",
                Descricao = "",
                OcorreuAlgumErro = false,
                UrlRedirecionar = string.Empty
            });
        }



        public IActionResult Requisicao() {
            return View();
        }
    }
}
