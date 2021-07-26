using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oasis.Dados;
using Oasis.Dominio.Entidades;
using Oasis.Web.Areas.Direcao.ViewModels;
using Oasis.Web.Extensions;
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
            Notificacao notificacaoProfessorAssociadoGrupo = new()
            {
                Titulo = $"Foi adicionado ao grupo {disciplinasViewModel.Grupo.Nome}!",
                LinkDestino = string.Empty,
                ApplicationUserId = disciplinasViewModel.ProfessorId
            };

            foreach(TipoPost flair in _context.GetTiposPostsDefault())
            {
                disciplinasViewModel.Grupo.Flairs.Add(flair);
            }
            


            _context.Notificacoes.Add(notificacaoProfessorAssociadoGrupo);
            _context.Grupos.Add(disciplinasViewModel.Grupo);
            await _context.SaveChangesAsync();

            return Json(new Ajax
            {
                Titulo = "Sucesso ao adicionar o grupo!",
                Descricao = "Com o grupo criado, já poderá associar os respetivos alunos ao mesmo.",
                OcorreuAlgumErro = false,
                UrlRedirecionar = string.Empty
            });
        }
    }
}