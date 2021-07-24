using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oasis.Dados;
using Oasis.Web.Areas.Administrador.ViewModels;
using Oasis.Web.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Administrador.Controllers
{
    public class TemasController : BaseAdministradorController
    {
        private readonly OasisContext _context;
        public TemasController(OasisContext context) => (_context) = (context);

        [HttpGet]
        public async Task<ViewResult> Index() => View(model: new TemasViewModel
        {
            Temas = await _context.Temas
                                  .AsNoTracking()
                                  .OrderBy(tema => tema.Nome)
                                  .ToListAsync()
        });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Editar([FromForm] TemasViewModel temasViewModel)
        {
            if (!(ModelState.IsValid))
            {
                return Json(new Ajax
                {
                    Titulo = "Erro ao atualizar o tema!",
                    Descricao = "Os dados indicados não se encontram num formato válido!",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            _context.Temas.Update(temasViewModel.TemaAtualizar);
            await _context.SaveChangesAsync();

            return Json(new
            {
                Ajax = new Ajax
                {
                    Titulo = "Sucesso ao atualizar o tema!",
                    Descricao = "O tema foi atualizado com sucesso.",
                    OcorreuAlgumErro = false,
                    UrlRedirecionar = string.Empty
                },
                Tema = temasViewModel.TemaAdicionar
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Criar([FromForm] TemasViewModel temasViewModel)
        {
            if (!(ModelState.IsValid))
            {
                return Json(new Ajax
                {
                    Titulo = "Erro ao adicionar o tema!",
                    Descricao = "Os dados indicados não se encontram num formato válido!",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            _context.Temas.Add(temasViewModel.TemaAdicionar);
            await _context.SaveChangesAsync();

            return Json(new
            {
                Ajax = new Ajax
                {
                    Titulo = "Sucesso ao adicionar o tema!",
                    Descricao = "O tema foi adicionado com sucesso.",
                    OcorreuAlgumErro = false,
                    UrlRedirecionar = string.Empty
                },
                Tema = temasViewModel.TemaAdicionar
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Eliminar([FromForm] TemasViewModel temasViewModel)
        {
            if (!(ModelState.IsValid))
            {
                return Json(new Ajax
                {
                    Titulo = "Erro ao eliminar o tema!",
                    Descricao = "Os dados indicados não se encontram num formato válido!",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            var temaParaEliminar = await _context.Temas
                                                 .FindAsync(temasViewModel.TemaEliminarId);

            if (temaParaEliminar is null)
            {
                return Json(new Ajax
                {
                    Titulo = "Ocorreu um erro na inserção do membro da direção!",
                    Descricao = "Pedimos desculpa pelo incómodo. Já foi enviado a informação aos nossos técnicos. Por favor, tente novamente mais tarde.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }


            _context.Temas.Remove(temaParaEliminar);
            await _context.SaveChangesAsync();

            return Json(new Ajax
            {
                Titulo = "Sucesso ao eliminar o tema!",
                Descricao = "O tema eliminado com sucesso.",
                OcorreuAlgumErro = false,
                UrlRedirecionar = string.Empty
            });
        }




        [HttpGet("[area]/[controller]/[action]/{id}")]
        public async Task<JsonResult> Id(int id) => Json(await _context.Temas.FindAsync(id));
    }
}
