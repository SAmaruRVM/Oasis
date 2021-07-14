using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oasis.Dados;
using Oasis.Web.Areas.Administrador.ViewModels;
using Oasis.Web.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class EscolasController : Controller
    {
        private readonly OasisContext _context;
        public EscolasController(OasisContext context) => (_context) = (context);

        [HttpGet]
        public async Task<ViewResult> Index() => View(model: new EscolasViewModel
        {
            Escolas = await _context.Escolas
                                    .AsNoTracking()
                                    .OrderBy(escola => escola.Nome)
                                    .ToListAsync()
        });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AdicionarEscola([FromForm] EscolasViewModel escolasViewModel) 
        {
            if(!(ModelState.IsValid))
            {
                return Json(new Ajax 
                {
                    Titulo = "Erro ao adicionar a escola!",
                    Descricao = "Os dados indicados não se encontram num formato válido!",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            _context.Escolas.Add(escolasViewModel.EscolaAdicionar);
            await _context.SaveChangesAsync();



            return Json(new
            {
                Ajax = new Ajax
                {
                    Titulo = "Sucesso ao adicionar a escola!",
                    Descricao = "Por favor, para facilitar a gestão da escola, comece a adicionar as pessoas responsávels pela sua direção.",
                    OcorreuAlgumErro = false,
                    UrlRedirecionar = string.Empty
                },
                Escola = escolasViewModel.EscolaAdicionar
            });
        }
    }
}
