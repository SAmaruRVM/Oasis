using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oasis.Dados;
using System.Threading.Tasks;

namespace Oasis.Web.Controllers
{
    public class TemasController : Controller
    {

        private readonly OasisContext _context;
        public TemasController(OasisContext context) => (_context) = (context);
        public async Task<ViewResult> Index() => View(model: await _context.Temas.AsNoTracking().ToListAsync());
    }
}
