using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oasis.Dados;
using Oasis.Web.Areas.Direcao.ViewModels;
using Oasis.Web.Http;

namespace Oasis.Web.Areas.Direcao.Controllers
{
    public class GruposController : BaseDirecaoController
    {
        private readonly OasisContext _context;
        public GruposController(OasisContext context) => (_context) = (context);


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Criar([FromForm] DisciplinasViewModel disciplinasViewModel)
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

             disciplinasViewModel.Grupo.DisciplinaId = disciplinasViewModel.DisciplinaId;
             disciplinasViewModel.Grupo.ProfessorId = disciplinasViewModel.ProfessorId;

             _context.Grupos.Add(disciplinasViewModel.Grupo);
             await _context.SaveChangesAsync();

            return Json(new Ajax
            {
                Titulo = "Sucesso ao adicionar o grupo!",
                Descricao = "Com o grupo criado, já poderá .",
                OcorreuAlgumErro = false,
                UrlRedirecionar = string.Empty
            });
        }
    }
}