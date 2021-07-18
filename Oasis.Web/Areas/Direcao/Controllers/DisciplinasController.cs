using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oasis.Dados;
using Oasis.Dominio.Entidades;
using Oasis.Web.Http;

namespace Oasis.Web.Areas.Direcao.Controllers
{
    [Area("Direcao")]
    public class DisciplinasController : Controller
    {
        private readonly OasisContext _context;
        public DisciplinasController(OasisContext context) => (_context) = (context);


        [HttpGet]
        public ViewResult Index() => View();


        [HttpGet]
        public ViewResult Criar() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Criar([FromForm] Disciplina disciplina)
        {
            if (!(ModelState.IsValid))
            {
                return Json(new Ajax
                {
                    Titulo = "Os dados indicados não se encontram num formato válido!",
                    Descricao = "Por favor, introduza os dados corretamente.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            disciplina.CriadorDirecao = await _context.Utilizadores
                                                 .Include(utilizador => utilizador.Escola)
                                                 .SingleOrDefaultAsync(utilizador => utilizador.Email == User.Identity.Name);

            disciplina.Escola = disciplina.CriadorDirecao.Escola;

            _context.Disciplinas.Add(disciplina);
            await _context.SaveChangesAsync();

            return Json(new Ajax
            {
                Titulo = "Sucesso ao adicionar a disciplina!",
                Descricao = "Com a disciplina criada, já poderá criar grupos e sucessivamente os respetivos membros.",
                OcorreuAlgumErro = false,
                UrlRedirecionar = string.Empty
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Atualizar([FromForm] Disciplina disciplina) => Json(string.Empty);
    }
}
