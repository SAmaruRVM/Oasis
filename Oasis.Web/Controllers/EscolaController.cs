using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Oasis.Dados;
using Oasis.Dominio.Entidades;
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
        public async Task<IActionResult> Grupo(string nomeGrupo)
        {
            var nomeGrupoSql = string.Join(' ', nomeGrupo.Split('-'));

            var utilizadorLogado = await _context.GetLoggedInApplicationUser(User.Identity.Name);


            var grupo = await _context.Grupos
                                      .AsNoTracking()
                                      .Include(grupo => grupo.Posts)
                                      .ThenInclude(post => post.UtilizadoresQueGostaram)
                                      .Include(grupo => grupo.Posts)
                                      .ThenInclude(post => post.TipoPost)
                                      .Include(grupo => grupo.Posts)
                                      .ThenInclude(post => post.Criador)
                                      .Include(grupo => grupo.Disciplina)
                                      .Include(grupo => grupo.Flairs)
                                      .SingleOrDefaultAsync(grupo => grupo.Nome == nomeGrupoSql && grupo.Disciplina.Escola.Id == utilizadorLogado.Escola.Id);

            if (grupo is null)
            {
                return NotFound();
            }


            var disciplinaGruposAlunos = utilizadorLogado.GruposOndeEnsina
                                                         .GroupBy(grupo => grupo.Disciplina.Nome)
                                                         .Select(disciplina => new DisciplinaGruposAlunos
                                                         {
                                                             NomeDisciplina = disciplina.Key,
                                                             GruposOndeEnsina = disciplina
                                                         })
                                                         .OrderBy(disciplina => disciplina.NomeDisciplina);


            var tiposPostDropdownList = grupo.Flairs
                                             .OrderBy(flair => flair.Nome)
                                             .Select(flair => new SelectListItem(flair.Nome, flair.Id.ToString()));

            var postsGrupo = grupo.Posts
                                  .OrderByDescending(post => post.DataCriacao);


            return View(model: new GrupoDisciplinaViewModel
            {
                EscolaViewModel = new EscolaViewModel
                {
                    DisciplinaGruposAlunos = disciplinaGruposAlunos
                },
                Reacoes = await _context.Reacoes
                                        .AsNoTracking()
                                        .ToListAsync(),
                Grupo = grupo,
                TiposPostDropdownList = tiposPostDropdownList,
                PostsGrupo = postsGrupo
            });
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

            return Json(new
            {
                Ajax = new Ajax
                {
                    Titulo = "O grupo foi alterado com sucesso!",
                    Descricao = string.Empty,
                    OcorreuAlgumErro = false,
                    UrlRedirecionar = string.Empty
                },
                GrupoAtualizado = grupoParaAtualizar
            });
        }


        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AdicionarPost([FromForm] PostInserirViewModel postInserirViewModel)
        {
            if (!(ModelState.IsValid) || (await _context.TiposPosts.FindAsync(postInserirViewModel.Post.TipoPostId)) is null)
            {
                return Json(new Ajax
                {
                    Titulo = "Os dados indicados não se encontram num formato válido!",
                    Descricao = "Por favor, introduza os dados corretamente.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            var grupo = await _context.Grupos
                                      .Include(grupo => grupo.Professor)
                                      .SingleOrDefaultAsync(grupo => grupo.Id == postInserirViewModel.Post.GrupoId);


            grupo.Professor.Notificacoes.Add(new Notificacao
            {
                Titulo = $"Um novo post foi criado no grupo {grupo.Nome}",
                LinkDestino = string.Empty
            });



            postInserirViewModel.Post.ApplicationUserId = (await _context.GetLoggedInApplicationUser(User.Identity.Name)).Id;




            _context.Posts.Add(postInserirViewModel.Post);
            await _context.SaveChangesAsync();

            return Json(new
            {
                Ajax = new Ajax
                {
                    Titulo = "O post foi inserido com sucesso!",
                    Descricao = string.Empty,
                    OcorreuAlgumErro = false,
                    UrlRedirecionar = string.Empty
                },
                Post = postInserirViewModel.Post
            });
        }



        [HttpGet("[action]/Id/{id}")]
        public async Task<JsonResult> Grupo(int id) => Json(await _context.Grupos.FindAsync(id));
    }
}