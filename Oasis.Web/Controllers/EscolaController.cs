using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oasis.Dados;
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
        [Route("[action]/{nomeEscola}")]
        [Route("{nomeEscola}")]
        public async Task<IActionResult> Index(string nomeEscola)
        {
            string nomeEscolaSql = string.Join(' ', nomeEscola.Split('-'));

            var utilizadorLogado = await _context.Utilizadores
                                               .AsNoTracking()
                                               .SingleOrDefaultAsync(utilizador => utilizador.Email == User.Identity.Name && nomeEscolaSql == utilizador.Escola.Nome);

            if (utilizadorLogado is null) 
            {
                return Forbid();
            }

            return View();
        }
    }
}   