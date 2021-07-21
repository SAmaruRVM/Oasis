using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oasis.Dados;
using Oasis.Dominio.Entidades;
using Oasis.Web.Areas.Direcao.ViewModels;
using Oasis.Web.Extensions;
using Oasis.Web.Http;

namespace Oasis.Web.Areas.Direcao.Controllers
{

    public class DisciplinasController : BaseDirecaoController
    {
        private readonly OasisContext _context;
        public DisciplinasController(OasisContext context) => (_context) = (context);


        [HttpGet]
        public async Task<ViewResult> Index()
           => View(model: new DisciplinasViewModel
           {
               Disciplinas = (await _context.GetLoggedInApplicationUser(User.Identity.Name)).Escola.Disciplinas.AsEnumerable()
           });




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
        public async Task<JsonResult> Eliminar([FromForm] int idDisciplina)
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

            var disciplinaParaEliminar = await _context.Disciplinas.FindAsync(idDisciplina);

            if(disciplinaParaEliminar is null)
            {
                return Json(new Ajax
                {
                    Titulo = "Ocorreu um erro na eliminação da disciplina selecionada!",
                    Descricao = "Pedimos desculpa pelo incómodo. Já foi enviado a informação aos nossos técnicos. Por favor, tente novamente mais tarde.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            _context.Disciplinas.Remove(disciplinaParaEliminar);
            await _context.SaveChangesAsync();

            return Json(new Ajax
            {
                Titulo = "Sucesso ao eliminar a disciplina!",
                Descricao = "A disciplina selecionada foi eliminada com sucesso.",
                OcorreuAlgumErro = false,
                UrlRedirecionar = string.Empty
            });
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Atualizar([FromForm] Disciplina disciplina) => Json(string.Empty);
    }
}
