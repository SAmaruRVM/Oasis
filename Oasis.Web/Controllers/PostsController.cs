using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oasis.Dados;
using Oasis.Web.Extensions;
using Oasis.Web.ViewModels;

namespace Oasis.Web.Controllers
{
    [Authorize(Roles = "Professor,Aluno")]
    public class PostsController : Controller
    {
        private readonly OasisContext _context;
        public PostsController(OasisContext context) => (_context) = (context);


        [HttpGet]
        public async Task<ViewResult> Guardados() => View(model: new PostsGuardadosViewModel
        {
            PostsGuardados = (await _context.GetLoggedInApplicationUser(User.Identity.Name))
                                                .PostsGuardados
                                                .OrderBy(postGuardado => postGuardado.Post.DataCriacao)
        });

        [HttpGet]
        public async Task<ViewResult> Reagidos() => View(model: new PostsGostadosViewModel
        {
            PostsGostados = (await _context.GetLoggedInApplicationUser(User.Identity.Name))
                                                .PostsGostados
                                                .OrderBy(postReagido => postReagido.Post.DataCriacao)
        });


        [HttpGet]

        public async Task<ViewResult> Publicados() => View(model: (await _context.GetLoggedInApplicationUser(User.Identity.Name))
                                                .PostsCriados
                                                .OrderBy(postPublicado => postPublicado.DataCriacao));
    }


}