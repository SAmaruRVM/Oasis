using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oasis.Dados;
using Oasis.Web.Areas.Administrador.ViewModels;
using Oasis.Web.ViewModels;

namespace Oasis.Web.Controllers
{
    [Route("[controller]")]
    public class EscolaController : Controller
    {
        private readonly OasisContext _context;
        public EscolaController(OasisContext context) => (_context) = (context);


        [HttpGet]
        [Route("[action]/{nomeEscola}")]
        [Route("{nomeEscola}")]
        public async Task<ViewResult> Index(string nomeEscola)
        {
            string nomeEscolaSql = string.Join(' ', nomeEscola.Split('-'));
            return View(model: new EscolaViewModel
            {
                Escola = await _context.Escolas
                                        .AsNoTracking()
                                        .Include(escola => escola.ConteudoPaginaPrincipal)
                                        .Include(escola => escola.Disciplinas)
                                        .SingleOrDefaultAsync(escola => nomeEscolaSql == nomeEscola)
            });
        }
    }
}   