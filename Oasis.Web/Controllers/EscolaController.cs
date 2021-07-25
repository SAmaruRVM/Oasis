using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oasis.Dados;
using Oasis.Web.Extensions;
using Oasis.Web.Http;
using Oasis.Web.ViewModels;

namespace Oasis.Web.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class EscolaController : Controller
    {
        private readonly OasisContext _context;
        public EscolaController(OasisContext context) => (_context) = (context);


        [HttpGet]
        [Route("[action]")]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var utilizadorLogado = await _context.GetLoggedInApplicationUser(User.Identity.Name);

            if (utilizadorLogado is null)
            {
                return Forbid();
            }

            var disciplinaGruposAlunos = utilizadorLogado.GruposOndeEnsina
                                   .GroupBy(grupo => grupo.Disciplina.Nome)
                                   .Select(disciplina => new DisciplinaGruposAlunos
                                   {
                                       NomeDisciplina = disciplina.Key,
                                       GruposOndeEnsina = disciplina
                                   })
                                   .OrderBy(disciplina => disciplina.NomeDisciplina);

            return View(model: new EscolaViewModel
            {
                DisciplinaGruposAlunos = disciplinaGruposAlunos
            });
        }

        [HttpGet("[action]/{nomeGrupo}")]
        public async Task<ViewResult> Grupo(string nomeGrupo)
        {
            var nomeGrupoSql = string.Join(' ', nomeGrupo.Split('-'));

            return View();
        }

        [HttpGet("[action]")]
        public async Task<ViewResult> Grupos()
        {
            var grupos = (await _context.GetLoggedInApplicationUser(User.Identity.Name))
                           .GruposOndeEnsina;

            GruposViewModel gruposViewModel = new()
            {
                Grupos = grupos
            };

            return View(model: gruposViewModel);
        }


        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditarGrupo([FromForm] GruposViewModel gruposViewModel)
        {
            var grupoParaAtualizar = await _context.Grupos.FindAsync(gruposViewModel.GrupoAlterar.Id);

            if (grupoParaAtualizar is null)
            {
                return Json(new Ajax
                {
                    Titulo = "Ocorreu um erro na atualização do grupo selecionado.",
                    Descricao = "Pedimos desculpa pela incómodo. Já foi enviado a informação aos nossos técnicos. Por favor, tente novamente mais tarde.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }


            grupoParaAtualizar.Descricao = gruposViewModel.GrupoAlterar.Descricao;
            _context.Grupos.Update(grupoParaAtualizar);
            await _context.SaveChangesAsync();

            return Json(new Ajax
            {
                Titulo = "O grupo foi alterado com sucesso!",
                Descricao = string.Empty,
                OcorreuAlgumErro = false,
                UrlRedirecionar = string.Empty
            });
        }



        [HttpGet("[action]/Id/{id}")]
        public async Task<JsonResult> Grupo(int id) => Json(await _context.Grupos.FindAsync(id));
    }
}