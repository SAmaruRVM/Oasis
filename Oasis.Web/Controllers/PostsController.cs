using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oasis.Dados;
using Oasis.Web.Extensions;

namespace Oasis.Web.Controllers
{
    public class PostsController : Controller
    {   
        private readonly OasisContext _context;
        public PostsController(OasisContext context) => (_context) = (context);


        [Authorize]
        public async Task<ViewResult> Guardados() 
        {
            var userLogado = await _context.GetLoggedInApplicationUser(User.Identity.Name);




            return View();
        }
    }
}