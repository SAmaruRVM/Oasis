using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oasis.Dados;
using Oasis.Web.Extensions;

namespace Oasis.Web.Controllers
{
    public class NotificacoesController : Controller
    {
        private readonly OasisContext _context;
        public NotificacoesController(OasisContext context) => (_context) = (context);

        [HttpGet]
        public async Task<ViewResult> Todas() => View(model:
             (await _context.GetLoggedInApplicationUser(User.Identity.Name))
                            .Notificacoes
        );
    }
}