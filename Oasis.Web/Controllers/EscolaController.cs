using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oasis.Dados;
using Oasis.Web.Extensions;
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
        public async Task<IActionResult> Index()
        {
            var utilizadorLogado = await _context.GetLoggedInApplicationUser(User.Identity.Name);

            if (utilizadorLogado is null) 
            {
                return Forbid();
            }

            var disciplinaGruposAlunos = utilizadorLogado.GruposOndeTemAulas
                                   .GroupBy(grupo => grupo.Grupo.Disciplina.Nome)
                                   .Select(disciplina => new DisciplinaGruposAlunos
                                   {
                                       NomeDisciplina = disciplina.Key, 
                                       Grupos = disciplina
                                   })
                                   .OrderBy(disciplina => disciplina.NomeDisciplina);

            return View(model: new EscolaViewModel 
            {
                DisciplinaGruposAlunos = disciplinaGruposAlunos
            });
        }
    }
}   